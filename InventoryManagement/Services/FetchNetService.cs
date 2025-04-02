using Newtonsoft.Json.Linq;

namespace InventoryManagement.Services
{
    public class FetchNetService
    {
        public static async Task<string> fetchNet()
        {
            string apiKey = "";  
            string searchEngineId = ""; 
            string query = "fairlife Lactose Free Reduced Fat Chocolate Ultra Filtered Milk, 52 fl oz price";

            string searchUrl = $"https://www.googleapis.com/customsearch/v1?q={Uri.EscapeDataString(query)}&key={apiKey}&cx={searchEngineId}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(searchUrl);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonData = JObject.Parse(jsonResponse);

                    foreach (var item in jsonData["items"])
                    {
                        string title = item["title"].ToString();
                        string link = item["link"].ToString();
                        string snippet = item["snippet"].ToString(); // This may contain the price

                        Console.WriteLine($"Title: {title}");
                        Console.WriteLine($"Link: {link}");
                        Console.WriteLine($"Snippet: {snippet}\n");

                        // Extract price from snippet
                        string price = ExtractPrice(snippet);

                        if (price == "Price not found")
                        {
                            Console.WriteLine("Snippet does not contain price. Fetching full webpage...");
                            //price = await GetPriceFromPage(link);
                        }

                        Console.WriteLine($"Final Extracted Price: {price}\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error fetching search results.");
                }

                return "test";
            }

        }

        static string ExtractPrice(string text)
        {
            System.Text.RegularExpressions.Match match =
                System.Text.RegularExpressions.Regex.Match(text, @"\$\d+(?:,\d{3})*(?:\.\d{2})?");
            return match.Success ? match.Value : "Price not found";
        }
    }
}
