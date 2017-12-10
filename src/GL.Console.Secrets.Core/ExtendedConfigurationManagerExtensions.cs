using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GL.Console.Secrets.Extensions
{
    public static class ExtendedConfigurationManagerExtensions
    {
        public static void AddSecretsFromJson(this ExtendedConfigurationManager manager, string secretsPath)
        {
            if (!File.Exists(secretsPath)) throw new FileNotFoundException("Unable to find secrets file", secretsPath);

            using (var streamReader = new StreamReader(secretsPath))
            {
                AddSecretsImpl(manager, streamReader.ReadToEnd());
            }
        }

        public static void AddSecretsFromEndpoint(this ExtendedConfigurationManager manager, string endpointUrl)
        {
            if (!Uri.TryCreate(endpointUrl, UriKind.Absolute, out Uri uriResult) || (uriResult?.Scheme != Uri.UriSchemeHttp && uriResult?.Scheme != Uri.UriSchemeHttps))
                throw new UriFormatException("Invalid endpoint URL provided");

            using (var client = new HttpClient())
            {
                var httpJsonResponse = client.GetStringAsync(new Uri(endpointUrl)).GetAwaiter().GetResult();
                AddSecretsImpl(manager, httpJsonResponse);
            }
        }

        private static void AddSecretsImpl(ExtendedConfigurationManager manager, string json)
        {
            if (string.IsNullOrEmpty(json)) throw new ArgumentException("Source JSON in empty");

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, string>), settings);
                var secrets = serializer.ReadObject(ms) as Dictionary<string, string>;
                if (secrets != null)
                {
                    foreach (var secretItem in secrets)
                    {
                        if (manager.Secrets[secretItem.Key] == null)
                        {
                            manager.Secrets.Add(secretItem.Key, secretItem.Value);
                        }
                    }
                }
            }
        }
    }
}
