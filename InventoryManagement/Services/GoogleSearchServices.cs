using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Http;
using System.Text.Json;

namespace InventoryManagement.Services
{
    public class GoogleSearchServices
    {
        public static string apiKey = "";
        public static string searchEngineId = "";
        public static string query = "fairlife Lactose Free Reduced Fat Chocolate Ultra Filtered Milk, 52 fl oz price";

        public static string searchUrl = $"https://www.googleapis.com/customsearch/v1?q={Uri.EscapeDataString(query)}&key={apiKey}&cx={searchEngineId}";


        public async Task<string> googleSearchGetAsync(string question, string prompt = "You are a helpful assistant to get the price of a product from the Internet")
        {
            string ans = "";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(searchUrl);
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Source", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Date", typeof(DateTime));
                
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonData = JObject.Parse(jsonResponse);

                    foreach (var item in jsonData["items"])
                    {
                        string title = item["title"].ToString();
                        string link = item["link"].ToString();
                        string snippet = item["snippet"].ToString(); 

                        Console.WriteLine($"Title: {title}");
                        Console.WriteLine($"Link: {link}");
                        Console.WriteLine($"Snippet: {snippet}\n");

                        // Extract price using regex
                        string price = ExtractPrice(snippet);
                        if (price != "Price not found")
                        {
                            Console.WriteLine($"Extracted Price: {price}\n");

                            dt.Rows.Add(title, price, DateTime.Now);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error fetching search results.");
                }
            }


            return ans;
        }

        public static string ExtractPrice(string text)
        {
            System.Text.RegularExpressions.Match match =
                System.Text.RegularExpressions.Regex.Match(text, @"\$\d+(?:,\d{3})*(?:\.\d{2})?");
            return match.Success ? match.Value : "Price not found";
        }
    }
}
