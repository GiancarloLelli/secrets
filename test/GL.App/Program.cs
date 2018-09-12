using GL.Console.Secrets;
using GL.Console.Secrets.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GL.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var ext = new ExtendedConfigurationManager();
            ext.AddSecretsFromEndpoint("https://jsonplaceholder.typicode.com/posts/1");
            ext.AddSecretsFromJson("secretsTest.json");
            ext.AddSecretFromProtectedEndpointAsync("https://jsonplaceholder.typicode.com/posts/1", GetToken, ParseResponseMessage);

            // All the settings are now in the Secret collection
            System.Console.WriteLine($"Settings: {ext.Secrets["title"]}");
        }

        static async Task<string> GetToken()
            => await Task.FromResult("Bearer: OAuth20_Token");

        static async Task<Dictionary<string, string>> ParseResponseMessage(HttpResponseMessage response)
            => await Task.FromResult(new Dictionary<string, string> { { "SecretKey", "SecretValue" } });
    }
}