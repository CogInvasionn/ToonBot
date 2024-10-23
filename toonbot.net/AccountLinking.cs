using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace toonbot.net
{
    public static class ApiAccountLinking
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<string> LinkAccountAsync(string discordUsername, string uuid)
        {
            string apiUrl = "https://api.coginvasion.lol/v1/account/link";

            var requestData = new
            {
                discord_username = discordUsername,
                uuid = uuid
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return result;  // Parse this as needed
            }
            else
            {
                return "Failed to link account via API.";
            }
        }
    }
}
