using System;

namespace GMS.WTP.CSVParser
{
    public static class EnvironmentVariables
    {
        public static string WTP_SERVICE_BUS_CONNECTION_STRING { get { return GetEnvironmentVariable(nameof(WTP_SERVICE_BUS_CONNECTION_STRING)); } }

        private static string GetEnvironmentVariable(string environmentVariableKey) => Environment.GetEnvironmentVariable(environmentVariableKey) ?? throw new Exception($"{environmentVariableKey} environment variable is null!");

    }
}
