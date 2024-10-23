using Discord.Commands;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;

namespace toonbot.commands
{
    public static class RaceWinDataCommand
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string ApiUrl = "https://api.coginvasion.lol/v1/race/windata";  // Update with the correct API endpoint

        public static async Task HandleRaceWinDataCommandAsync(SocketCommandContext context, string[] args)
        {
            if (args.Length > 1 && args[1] == "cf73c0c2-99a6-4566-8713-f4272e6b07e0") // Example UUID
            {
                var response = await FetchRaceWinDataAsync(args[1]);

                if (response != null)
                {
                    var embed = new EmbedBuilder()
                        .WithColor(0x3498db) // Blue color
                        .WithTitle("Race Win Data for Pumpkin Splat")
                        .WithDescription($"Account owner: {response.Owner}\n(UUID: {args[1]})")
                        .AddField("Total Wins", response.TotalWins.ToString(), true)
                        .AddField("Total Races", response.TotalRaces.ToString(), true)
                        .AddField("Win Rate", response.WinRate.ToString("P"), true)
                        .WithTimestamp(System.DateTimeOffset.Now)
                        .WithFooter("Cog Invasion Race Statistics", "https://example.com/cog-icon.png");

                    await context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    await context.Channel.SendMessageAsync("Invalid race UUID for win data.");
                }
            }
            else
            {
                await context.Channel.SendMessageAsync("Invalid race UUID.");
            }
        }

        private static async Task<RaceWinDataResponse> FetchRaceWinDataAsync(string uuid)
        {
            var response = await _httpClient.GetAsync($"{ApiUrl}/{uuid}");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RaceWinDataResponse>(json);
            }
            return null;
        }
    }

    public class RaceWinDataResponse
    {
        public string Owner { get; set; }
        public int TotalWins { get; set; }
        public int TotalRaces { get; set; }
        public double WinRate { get; set; }
    }
}
