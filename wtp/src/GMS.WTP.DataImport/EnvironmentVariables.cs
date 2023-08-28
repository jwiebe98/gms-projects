using System;

namespace GMS.WTP.DataImport
{
    public static class EnvironmentVariables
    {
        public static string DB_CIMS { get { return GetEnvironmentVariable(nameof(DB_CIMS)); } }
        private static string GetEnvironmentVariable(string environmentVariableKey) => Environment.GetEnvironmentVariable(environmentVariableKey) ?? throw new Exception($"{environmentVariableKey} environment variable is null!");
    }
}
