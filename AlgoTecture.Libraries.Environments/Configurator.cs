using Microsoft.Extensions.Configuration;

namespace AlgoTecture.Libraries.Environments
{
    public static class Configurator
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}