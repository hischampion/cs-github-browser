using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    public class GithubClient
    {
        private HttpClient client;
        
        public GithubClient()
        {
            client = new HttpClient();

            // add custom github headers for JSON access
            var headers = client.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            headers.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        /**
         * Get the repos for a given owner.
         */
        public async Task<SearchResult> PerformSearch(EscapedString query)
        {
            var root = "https://api.github.com/search/repositories";
            var parameter = "?q=";

            var serializer = new DataContractJsonSerializer(typeof(SearchResult));

            var streamTask = client.GetStreamAsync(root + parameter + query);

            return serializer.ReadObject(await streamTask) as SearchResult;
        }

        /**
         * Get the repos for a given owner.
         */
        public async Task<List<Repository>> ProcessRepositories(String owner)
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            var url = String.Format("https://api.github.com/orgs/{0}/repos", owner);

            var streamTask = client.GetStreamAsync(url);

            return serializer.ReadObject(await streamTask) as List<Repository>;
        }

        /**
         * Prints the first 500 characters of the stream to console.
         * Useful if you have no idea what a http request is returning.
         */
        private async void debugHttp(Task<Stream> streamTask)
        {
            var reader = new StreamReader(await streamTask);

            for (var i = 0; i < 500; i++)
            {
                Console.Write((char)reader.Read());
            }
        }
    }
}