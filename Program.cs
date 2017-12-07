using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Debug.WriteLine("Hello World!");
            var repos = ProcessRepositories().Result;

            foreach (var repo in repos)
            {
                System.Diagnostics.Debug.WriteLine(repo.Name + " - " + repo.LastPush);
            }
        }

        private static async Task<List<Repository>> ProcessRepositories()
        {
            var client = new HttpClient();
            var headers = client.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            headers.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            return serializer.ReadObject(await streamTask) as List<Repository>;
        }
    }
}
