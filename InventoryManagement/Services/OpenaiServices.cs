using System.Data;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.Services
{
    public class OpenaiServices
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenaiServices(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = "";
            _model = "gpt-3.5-turbo";
            
        }

        public async Task<string> GetAnswerAsync(string question, string prompt = "You are a helpful assistant to get the price of a product from the Internet")
        {
            // Step 1: Perform a web search for real-time prices (Google/Bing API, etc.)
            // string searchResult = await SearchApi.GetProductPriceAsync(productName);
                      

            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                new { role = "system", content =  prompt},
                new { role = "user", content = question }
            },
                max_tokens = 150
            };

            var requestJson = JsonSerializer.Serialize(requestBody);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            var responseJson = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseJson);

            string ans = jsonDoc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();

            return ans;
        }

        public async Task<string> summaryDaily(string question)
        {
            DatabaseService dbHelper = new DatabaseService("Server=RAY\\SQLEXPRESS;Database=InvMgnt;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

            string query = $@"SELECT format(A.[CreateTime], 'MM/dd/yyyy')
                                    ,B.[StoreName]
	                                ,C.[ProductName]
                                    ,SUM([quantity]) as 'quantity'
                                FROM [InvMgnt].[dbo].[CustomerPurchase] A
                               INNER JOIN [InvMgnt].[dbo].[Store] B
                                  ON A.[StoreId] = B.[StoreId]
                               INNER JOIN [InvMgnt].[dbo].[Products] C
                                  ON A.[productid] = C.[productid]
                               where A.[StoreId] in ('Sid01', 'Sid02', 'Sid03')
                               --  and format([CreateTime], 'MM/dd/yyyy') = format(getdate(), 'MM/dd/yyyy')
                               GROUP BY format(A.[CreateTime], 'MM/dd/yyyy'), B.[StoreName], C.[ProductName]";

            DataTable dt = dbHelper.connectDb(query);

            string summarySales = "{'store Name', 'product Name', 'Quantity': [";

            if (dt?.Rows?.Count > 0 ) 
            { 
                for ( int i = 0; i < dt?.Rows?.Count; i++ )
                { 
                    summarySales +=$@"({dt.Rows[i][0]},{dt.Rows[i][1]},{dt.Rows[i][2]})," ;
                }
            
            }
            summarySales += "]}";

            string result = await GetAnswerAsync(summarySales, "You are helping retailers summarize daily performace");

            return "";
        }
    }
}
