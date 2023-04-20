using AppExample.Configuration;

using Microsoft.Extensions.Configuration;

namespace AppExample.Inizializers;

public class ConfigInitializer
{
    private readonly string[] _argsArray;

    public ConfigInitializer()
    {
        _argsArray = new[] { "q", "d", "p" };
    }

    public (IConfiguration congig, string environment) GetConfiguration()
    {
        var input = string.Empty;
        dynamic config = null!;

        do
        {
            Console.WriteLine($"Введите:{Environment.NewLine}" +
                    $"\"d\" для запуска конфигурации разработки{Environment.NewLine}" +
                    $"\"p\" для запуска конфигурации релиза{Environment.NewLine}" +
                    $"\"q\" для выхода из приложения");

            input = Console.ReadLine();
            config = Config.GetConfiguration(input);

            if (config is null)
                return (null, null)!;

        } while (!_argsArray.Contains(input));

        return config;
    }
}