using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Extensions
{
    public static class AzureAppConfigurationExtensions
    {
        public static void ConfigureAppConfiguration(this IConfigurationBuilder builder, AzureAppConfigOptions options)
        {
            if (options is null) throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrEmpty(options.Endpoint)) throw new ArgumentNullException(nameof(options.Endpoint));
            if (!options.PrefixNames.Any()) throw new ArgumentNullException(nameof(options.PrefixNames));
            builder.AddAzureAppConfiguration(opts =>
            {
                ConnectAzureConfigService(options, opts);
            });
        }

        private static void ConnectAzureConfigService(AzureAppConfigOptions options, AzureAppConfigurationOptions opts)
        {
            foreach (var prefix in options.PrefixNames)
            {
                opts.Connect(new Uri(options.Endpoint), options.AzureCredential)
                .Select($"{prefix}:*")
                .TrimKeyPrefix($"{prefix}:")
                .ConfigureKeyVault(kv => kv.SetCredential(options.AzureCredential));
            }
        }
    }

    public class AzureAppConfigOptions
    {
        public string Endpoint { get; set; } = string.Empty;

        public string[] PrefixNames { get; set; } = Array.Empty<string>();

        public DefaultAzureCredential AzureCredential { get; set; } = new DefaultAzureCredential();
    }
}