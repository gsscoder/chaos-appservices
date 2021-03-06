using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient() {
            // It's good practice to have a paramtric timeout
            Timeout = TimeSpan.FromMilliseconds(Settings.HttpTimeout)
        };
        var endPoint = new UriBuilder($"https://api.applicationinsights.io/v1/apps/{Settings.AIApplicationId}/events/traces");
        endPoint.Query = $"timespan=P{Settings.Days}D";
        var request = new HttpRequestMessage() {
            RequestUri = endPoint.Uri,
            Method = HttpMethod.Get
        };
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("x-api-key", Settings.AIApiKey);
        Stream stream = null;
        await client.SendAsync(request)
            .ContinueWith(async withMessage => {
                stream = (await withMessage.Result.Content.ReadAsStreamAsync());
            });
        var json = JObject.Parse(new StreamReader(stream).ReadToEnd());
        Console.WriteLine(json);
    }
}