using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.CrossCutting.Utils
{
    public class VarEnvHelper
    {
        private static string? Get(string name, bool connectionString = false)
        {
            ConfigurationBuilder configurationBuilder = new();
            configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = configurationBuilder.Build();

            if (connectionString)
            {
                string? value = configuration.GetSection("ConnectionStrings").GetSection(name).Value;
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            if (configuration.AsEnumerable().Any(x => x.Key == name) && !connectionString)
            {
                return configuration[name];
            }

            return Environment.GetEnvironmentVariable(name);
        }

        public static string? GetEnvironmentVariable(string name)
        {
            return Get(name);
        }

        public static string? GetEnvironmentVariable(string name, string defaultValue)
        {
            return Get(name) ?? defaultValue;
        }

        public static string? GetConnectionString(string name)
        {
            return Get(name, true);
        }
    }
}
