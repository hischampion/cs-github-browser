using System;
using System.Collections.Generic;
using System.IO;
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
            while (true)
            {
                Console.Write("Enter a search query:");
                var query = Console.ReadLine(); // clean?

                // only returns 30 at a time
                SearchResult result = PerformSearch(query).Result;

                foreach (var repo in result.items)
                {
                    Console.WriteLine(repo.Name + "\t\t" + repo.LastPush);
                }

                // let the user know if there are more hidden results
                var complete = result.Complete ? "some" : "all";
                Console.WriteLine("\nShowing " + complete + " results: " + result.Count + " count\n\n");
            }
        }

        /**
         * Get the repos for a given owner.
         */
        private static async Task<SearchResult> PerformSearch(String query)
        {
            var client = new HttpClient();
            var root = "https://api.github.com/search/repositories";
            var parameter = "?q=";

            // add custom github headers for JSON access
            var headers = client.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            headers.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(SearchResult));

            var response = await client.GetAsync(root + parameter + query, HttpCompletionOption.ResponseHeadersRead);

            var streamTask = client.GetStreamAsync(root + parameter + query);

            //var reader = new StreamReader(await streamTask);

            //for (var i = 0; i < 500; i++)
            //{
            //    Console.Write((char)reader.Read());
            //}

            return serializer.ReadObject(await streamTask) as SearchResult;
        }

        /**
         * Get the repos for a given owner.
         */
        private static async Task<List<Repository>> ProcessRepositories(String owner)
        {
            var client = new HttpClient();

            // add custom github headers for JSON access
            var headers = client.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            headers.Add("User-Agent", ".NET Foundation Repository Reporter");

            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var url = String.Format("https://api.github.com/orgs/{0}/repos", owner);

            var streamTask = client.GetStreamAsync(url);

            return serializer.ReadObject(await streamTask) as List<Repository>;
        }

        private void debugDotNetRepos()
        {
            System.Diagnostics.Debug.WriteLine("Hello World!");
            var repos = ProcessRepositories("dotnet").Result;

            foreach (var repo in repos)
            {
                System.Diagnostics.Debug.WriteLine(repo.Name + " - " + repo.LastPush);
            }
        }
    }
}
