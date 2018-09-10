using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GL.Console.Secrets.Core.Internal
{
    internal static class InternalHelper
    {
        public static void AddRange(ExtendedConfigurationManager manager, string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentException("Source JSON in empty");

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

        public static void AddRange(ExtendedConfigurationManager manager, Dictionary<string, string> secrets)
        {
            if (secrets == null)
                throw new ArgumentNullException(nameof(secrets));

            foreach (var secretItem in secrets)
            {
                manager.Secrets.Add(secretItem.Key, secretItem.Value);
            }
        }
    }
}
