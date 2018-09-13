using HtmlAgilityPack;
using System;
using System.Net.Http;

namespace HTMLParse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Make sure there is actually a URL to find images for. 
            if (args.Length > 0)
            {
                GetHtml(args[0]);

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Usage: HTMLParse <url> e.g. HTMLParse http://news.bbc.co.uk");
            }


        }

        static async void GetHtml(string url)
        {
            HttpClient client = new HttpClient();

            try
            {
                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    HtmlDocument htmldoc = new HtmlDocument();
                    htmldoc.LoadHtml(content);
                    var images = htmldoc.DocumentNode.SelectNodes("//img");

                    foreach (var image in images)
                    {
                        if (image.Attributes["src"] != null)
                        {
                            Console.WriteLine(image.Attributes["src"].Value);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Unable to retreive HTML");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         }
    }
}
