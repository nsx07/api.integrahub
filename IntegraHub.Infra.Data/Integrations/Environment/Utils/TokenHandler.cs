using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraHub.Infra.CrossCutting.Integrations.Environment.Utils
{
    public static class TokenHandler
    {

        /// <summary>
        /// Gets tokens from environment Eg: Dev (configuration) or Prod (envvar)
        /// <list type="table">
        ///     <item>Railway</item>
        ///     <item>GoDaddy</item>
        ///     <item>Vercel</item>
        /// </list>
        /// </summary>
        /// <param name="configuration"></param>
        public static Tokens Resolve(IConfiguration? configuration = null)
        {
            Tokens tokens = new()
            {
                Railway = GetToken("railway_token", configuration),
                Vercel = GetToken("vercel_token", configuration),
                GoDaddyKey = GetToken("godaddy_key", configuration),
                GoDaddySecret = GetToken("godaddy_secret", configuration)
            };

            return tokens;

        }

        public static string GetToken(string token, IConfiguration? configuration = null)
        {
            configuration ??= GetAppSettings();
            var environment = System.Environment.GetEnvironmentVariable(token, EnvironmentVariableTarget.Machine | EnvironmentVariableTarget.Process);
            return (environment ?? configuration.GetValue<string>(token))!;
        }

        public static IConfigurationRoot GetAppSettings()
        {
            string applicationExeDirectory = ApplicationExeDirectory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(applicationExeDirectory)
            .AddJsonFile("appsettings.Development.json");

            return builder.Build();
        }

        private static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot!;
        }


    }

    public record Tokens
    {
        public required string Vercel;
        public required string Railway;
        public required string GoDaddyKey;
        public required string GoDaddySecret;
    }
}
