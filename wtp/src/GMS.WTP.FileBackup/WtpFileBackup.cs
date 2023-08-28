using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GMS.WTP.FileBackup
{
    public static class WtpFileBackup
    {
        [FunctionName("WtpFileBackup")]
        public static async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"WTP Timer trigger function executed at: {DateTime.Now}");

            try
            {
                await CheckForNewSFTPFiles(log);
            }
            catch (Exception ex)
            {
                log.LogError($"Could not check for new files in SFTP server: {ex.Message}");
                log.LogError($"Stacktrace: {ex.StackTrace}");
            }

        }

        private static async Task CheckForNewSFTPFiles(ILogger log)
        {
            SftpClient sftpClient = SftpService.GetSftpClient();

            sftpClient.Connect();

            BlobContainerClient containerClient = GetBlobContainerClientAsync(log);

            List<string> filesInBlobStorage = await GetFileNamesInBlobStorage(log, containerClient);

            foreach (SftpFile file in sftpClient.ListDirectory(EnvironmentVariables.WTP_SFTP_SERVER_FILE_DIRECTORY))
            {
                if (!file.IsRegularFile)
                {
                    log.LogInformation($"'{file.Name}' is not a regular file, skipping.");
                }
                else if (filesInBlobStorage.Contains(file.Name))
                {
                    log.LogInformation($"File '{file.Name}' already exists in blob storage, skipping...");
                }
                else
                {
                    await HandleNewFileInSFTPServer(log, sftpClient, containerClient, file);
                }
            }

            sftpClient.Disconnect();
        }

        private static async Task HandleNewFileInSFTPServer(ILogger log, SftpClient sftpClient, BlobContainerClient containerClient, SftpFile file)
        {
            try
            {
                using (MemoryStream memoryStream = new())
                {
                    await sftpClient.DownloadFileAsync(file.FullName, memoryStream);
                    memoryStream.Position = 0;
                    using (var msWithUIDs = AddESBUIDToEachRowOfFile(memoryStream, log))
                    {
                        await SendFileToBlobStorage(containerClient, msWithUIDs, file, log);
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }

        private static MemoryStream AddESBUIDToEachRowOfFile(MemoryStream memoryStream, ILogger log)
        {
            log.LogInformation("Adding ESB message ID to each row of file");

            var outputStream = new MemoryStream();

            using (var reader = new StreamReader(memoryStream, leaveOpen: true))
            using (var writer = new StreamWriter(outputStream, leaveOpen: true))
            {
                var isFirstRow = true;
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isFirstRow)
                    {
                        writer.Write(line + ",ESBMessageID");
                        isFirstRow = false;
                    }
                    else
                    {
                        writer.Write(line + "," + Guid.NewGuid());
                    }

                    if (!reader.EndOfStream)
                    {
                        writer.WriteLine();
                    }
                }

                writer.Flush();
            }

            outputStream.Position = 0;
            return outputStream;
        }

        private static Task DownloadFileAsync(this SftpClient sftpClient, string path, MemoryStream memoryStream)
        {
            return Task.Run(() =>
            {
                sftpClient.DownloadFile(path, memoryStream);
            });
        }

        private static async Task SendFileToBlobStorage(BlobContainerClient containerClient, MemoryStream memoryStream, SftpFile file, ILogger log)
        {
            try
            {
                BlobClient blobClient = containerClient.GetBlobClient(file.Name);
                await blobClient.UploadAsync(memoryStream);
                log.LogInformation($"Copied new file '{file.Name}' to Azure Blob Storage as '{file.Name}'.");
            }
            catch (RequestFailedException ex) when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                log.LogError($"File with name {file.Name} already exists in container. Set another name to store the file in the container: '{containerClient.Name}.'");

            }
            catch (RequestFailedException ex)
            {
                LogError(log, ex, " Failed to upload file in blob storage. Check environment variables, network or firewall settings.");
            }
        }

        private static async Task<List<string>> GetFileNamesInBlobStorage(ILogger log, BlobContainerClient containerClient)
        {
            List<string> fileNames = new();
            try
            {
                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    fileNames.Add(blobItem.Name);
                }
            }
            catch (Exception ex)
            {
                LogError(log, ex, " Failed to get file names in blob storage. Blob container client information could be null");
            }

            return fileNames;
        }

        private static BlobContainerClient GetBlobContainerClientAsync(ILogger log)
        {
            try
            {
                BlobServiceClient blobServiceClient = new(EnvironmentVariables.WTP_STORAGE_ACCOUNT_CONNECTION_STRING);

                return blobServiceClient.GetBlobContainerClient(EnvironmentVariables.WTP_BLOB_CONTAINER_NAME);

            }
            catch (Exception ex)
            {
                LogError(log, ex, " Failed to get blob container client information. Check blob's environment variable.");
                return null;
            }

        }

        private static void LogError(ILogger log, Exception ex, string message)
        {
            message += $"Unhandled Exception.ID: {ex.StackTrace} - Message: {ex.Message}";
            log.LogError(message);
        }
    }
}
