using System;

namespace RinhaBackend
{
    public class EnvironmentConfig
    {
        public static class MongoConnection
        {
            public static readonly string Host = GetVariableValueOrDefault("MONGO_HOST", "localhost");
            public static readonly string User = GetVariableValueOrDefault("MONGO_USER", "admin");
            public static readonly string Secrete = GetVariableValueOrDefault("MONGO_SECRET", "secret");
            public static readonly string Port = GetVariableValueOrDefault("port", "27017");
        }

        private static string GetVariableValueOrDefault(string variableName, string defaultValue = "")
        {
            var value = Environment.GetEnvironmentVariable(variableName, EnvironmentVariableTarget.Process);

            return value ?? defaultValue;
        }
    }
}


