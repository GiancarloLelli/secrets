# Console Application Secrets Library
This repo contains .NET library that allows you to handle configuration settings in a flavour much alike the one offered by ASP.NET configuration secrets.
The foundation of this library is simply a wrapper around the static class **ConfigurationManager** contained in the namespace *System.Configuration*.
This library basically allows you inject into a custom collection called *Secrets* any settings either stored in a JSON file on you machine or returned by an HTTP/S endpoint.
In both cases, the JSON that is passed to the API should be a dictionary of key value pairs:

```json
{
  "Secret_One": "Test_One",
  "Secret_Two": "Test_Two",
  "Secret_Three": "Test_Three",
  "Secret_Four": "Test_Four",
  "Secret_Five": "Test_Five"
}
```
## Sample usage

```cs
class Program
{
	static void MainAsync(string[] args)
	{
		var extendedConf = new ExtendedConfigurationManager();
		extendedConf.AddSecretsFromJson("secretsTest.json");
		extendedConf.AddSecretsFromEndpoint("https://jsonplaceholder.typicode.com/posts/1");
		var three = extendedConf.Secrets["Secret_Three"];
		var title = extendedConf.Secrets["title"];
		Console.WriteLine($"Secret Three: {three}");
		Console.WriteLine($"Secret Title: {title}");
	}
}
```

## NuGet Package
A NuGet package is available [here](https://www.nuget.org/packages/GL.Console.Secrets)

## License and contact
This project is distributed under MIT License. If you have any question or you'd like to get involved in the project write to [gcarlo.lelli@live.com](mailto:gcarlo.lelli@live.com)
