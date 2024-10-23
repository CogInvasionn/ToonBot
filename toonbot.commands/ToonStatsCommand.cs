using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;

namespace toonbot.commands
{
    public static class ToonStatsCommand
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "https://api.coginvasion.lol/v1/stats";  // Update with the correct API endpoint

        public static async Task HandleToonStatsAsync(SocketCommandContext context, string[] args)
        {
            if (args.Length > 1)
            {
                string uuid = args[1];
                var response = await FetchToonStatsAsync(uuid);

                if (response != null)
                {
                    var embed = new EmbedBuilder()
                        .WithColor(0x3498db) // Blue color
                        .WithTitle($"Stats for {response.Name}")
                        .WithDescription($"Account owner: {response.Owner} (UUID: {uuid})")
                        .AddField("Cake Day", response.CakeDay, true)
                        .AddField("Laff", response.Laff.ToString(), true)
                        .AddField("Tasks Finished", response.TasksFinished.ToString(), true)
                        .WithTimestamp(System.DateTimeOffset.Now)
                        .WithFooter("Cog Invasion Statistics", "https://example.com/cog-icon.png");

                    await context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    await context.Channel.SendMessageAsync("No stats found for that Toon.");
                }
            }
            else
            {
                await context.Channel.SendMessageAsync("Please provide a valid UUID.");
            }
        }

        private static async Task<ToonStatsResponse> FetchToonStatsAsync(string uuid)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{uuid}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ToonStatsResponse>(json);
            }
            return null;
        }
    }

    public class ToonStatsResponse
    {
        public string Name { get; set; }
        public string Owner { get; set; }
        public string CakeDay { get; set; }
        public int Laff { get; set; }
        public int TasksFinished { get; set; }
    }
}
