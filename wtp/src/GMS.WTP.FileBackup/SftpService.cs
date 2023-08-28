using Renci.SshNet;
using System;
using System.IO;

namespace GMS.WTP.FileBackup
{
    public static class SftpService
    {
        public static SftpClient GetSftpClient()
        {
            return new(new ConnectionInfo(EnvironmentVariables.WTP_SFTP_SERVER_HOST_NAME, EnvironmentVariables.WTP_SFTP_SERVER_USER_NAME, new PrivateKeyAuthenticationMethod(EnvironmentVariables.WTP_SFTP_SERVER_USER_NAME, GetPrivateKeyFile())));
        }

        private static PrivateKeyFile GetPrivateKeyFile()
        {
            byte[] data = Convert.FromBase64String(EnvironmentVariables.WTP_SFTP_SERVER_PRIVATE_KEY);

            using (MemoryStream stream = new(data))
            {
                return new PrivateKeyFile(stream);
            }
        }
    }
}