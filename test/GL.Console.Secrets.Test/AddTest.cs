using GL.Console.Secrets.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace GL.Console.Secrets.Test
{
    [TestClass]
    public class AddTest
    {
        [TestMethod]
        public void Adding_Secrets_From_Existing_File()
        {
            try
            {
                var extendedConf = new ExtendedConfigurationManager();
                extendedConf.AddSecretsFromJson("secretsTest.json");
                Assert.IsNotNull(extendedConf.Secrets["Secret_Three"]);
                Assert.AreEqual(extendedConf.Secrets.Count, 5);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Adding_Secrets_From_Existing_Not_Existing_File_Throws()
        {
            var extendedConf = new ExtendedConfigurationManager();
            Assert.ThrowsException<FileNotFoundException>(() => extendedConf.AddSecretsFromJson("abracadabra.json"));
        }


        [TestMethod]
        public void Adding_Secrets_From_Http_Endpoint()
        {
            try
            {
                var extendedConf = new ExtendedConfigurationManager();
                extendedConf.AddSecretsFromEndpoint("https://jsonplaceholder.typicode.com/posts/1");
                Assert.IsNotNull(extendedConf.Secrets["title"]);
                Assert.AreEqual(extendedConf.Secrets.Count, 4);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Adding_Secrets_From_Invalid_URL_Throws()
        {
            var extendedConf = new ExtendedConfigurationManager();
            Assert.ThrowsException<UriFormatException>(() => extendedConf.AddSecretsFromEndpoint("C:\\not_http.json"));
            Assert.ThrowsException<UriFormatException>(() => extendedConf.AddSecretsFromEndpoint("ftp://mydomain.com/GetSettings"));
            Assert.ThrowsException<UriFormatException>(() => extendedConf.AddSecretsFromEndpoint("http://!!!!.com/GetSettings"));
            Assert.ThrowsException<UriFormatException>(() => extendedConf.AddSecretsFromEndpoint("nws://mysettingsdomain.com/GetSettings"));
        }
    }
}
