using Microsoft.Extensions.Configuration;

namespace AppExample.Configuration
{
    public static class Config
    {
        private static string DevelopmentConfig = "d";
        private static string Debug = "Development";
        private static string ProductionConfig = "p";
        private static string Release = "Production";

        public static (IConfiguration congig, string environment) GetConfiguration(string? argsArray, bool isService = false)
        {
            if (!isService && (string.IsNullOrWhiteSpace(argsArray) || argsArray.Equals("q", StringComparison.OrdinalIgnoreCase)))
                return (null, null)!;

            if (isService)
                argsArray = ProductionConfig;

            var environment = argsArray!.Equals(DevelopmentConfig, StringComparison.OrdinalIgnoreCase) ? Debug
                : (argsArray.Equals(ProductionConfig, StringComparison.OrdinalIgnoreCase) ? Release : null);

            var client = "Default";

            Console.WriteLine($"Client: {client}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"Clients/{client}/appsettings.{environment}.json", true, false);

            var config = builder.Build();

            return (config, environment)!;
        }
    }
}
