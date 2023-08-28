using System;

namespace GMS.WTP.FileBackup
{
    public static class EnvironmentVariables
    {
        public static string WTP_SFTP_SERVER_PRIVATE_KEY { get { return GetEnvironmentVariable(nameof(WTP_SFTP_SERVER_PRIVATE_KEY)); } }
        public static string WTP_SFTP_SERVER_HOST_NAME { get { return GetEnvironmentVariable(nameof(WTP_SFTP_SERVER_HOST_NAME)); } }
        public static string WTP_SFTP_SERVER_USER_NAME { get { return GetEnvironmentVariable(nameof(WTP_SFTP_SERVER_USER_NAME)); } }
        public static string WTP_SFTP_SERVER_FILE_DIRECTORY { get { return GetEnvironmentVariable(nameof(WTP_SFTP_SERVER_FILE_DIRECTORY)); } }
        public static string WTP_BLOB_CONTAINER_NAME { get { return GetEnvironmentVariable(nameof(WTP_BLOB_CONTAINER_NAME)); } }
        public static string WTP_STORAGE_ACCOUNT_CONNECTION_STRING { get { return GetEnvironmentVariable(nameof(WTP_STORAGE_ACCOUNT_CONNECTION_STRING)); } }

        private static string GetEnvironmentVariable(string environmentVariableKey) => Environment.GetEnvironmentVariable(environmentVariableKey) ?? throw new Exception($"{environmentVariableKey} environment variable is null!");

    }
}
