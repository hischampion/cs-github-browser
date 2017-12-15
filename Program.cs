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
                var query = Console.ReadLine();
                
                try
                {
                    // only returns 30 at a time
                    GithubClient client = new GithubClient();
                    SearchResult result = client.PerformSearch(new EscapedString(query)).Result;

                    if(result.Count == 0)
                    {
                        Console.WriteLine("No results found.\n");
                        continue;
                    }

                    // basic print of results TODO put this in columns
                    foreach (var repo in result.items)
                    {
                        Console.WriteLine(repo.Name + "\t\t" + repo.LastPush);
                    }

                    // let the user know if there are more hidden results
                    var complete = result.Complete ? "some" : "all";
                    Console.WriteLine("\nShowing " + complete + " results: " + result.Count + " count\n\n");
                }
                catch(Exception e)
                {
                    //TODO handle aggregate exceptions HttpResponseException
                    Console.WriteLine("Apologies, unable to reach the Github server: " + e.Message);
                }       
            }
        }

        private void debugDotNetRepos()
        {
            System.Diagnostics.Debug.WriteLine("Hello World!");
            GithubClient client = new GithubClient();
            var repos = client.ProcessRepositories(new EscapedString("dotnet")).Result;

            foreach (var repo in repos)
            {
                System.Diagnostics.Debug.WriteLine(repo.Name + " - " + repo.LastPush);
            }
        }
    }
}
