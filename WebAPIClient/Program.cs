using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebAPIClient
{
    class University
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("state-province")]
        public string Province { get; set; }

        [JsonProperty("alpha_two_code")]
        public string AlphaTwoCode { get; set; }

        [JsonProperty("web_pages")]
        public List<string> WebPages { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(String[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter University Search");

                    var searchInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(searchInput))
                    {
                        break;
                    }

                    var result = await client.GetAsync("http://universities.hipolabs.com/search?name=" + searchInput.ToLower());
                    var resultRead = await result.Content.ReadAsStringAsync();

                    var universities = JsonConvert.DeserializeObject<List<University>>(resultRead);
                    var university = universities[0];
                    
                    Console.WriteLine("---");
                    Console.WriteLine("Name: " + university.Name);
                    Console.WriteLine("Country: " + university.Country);
                    Console.WriteLine("Province: " + university.Province);
                    Console.WriteLine("AlphaTwoCode: " + university.AlphaTwoCode);
                    Console.WriteLine("Webpage(s):");
                    university.WebPages.ForEach(t => Console.Write(" " + t));
                    Console.WriteLine("\n---");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error");
                }

            }
        }
    }
}

