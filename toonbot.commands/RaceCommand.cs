using Discord.Commands;
using System.Threading.Tasks;

namespace toonbot.commands
{
    public static class RaceCommand
    {
        private const string RaceUUID = "cf73c0c2-99a6-4566-8713-f4272e6b07e0"; // Example UUID

        public static async Task HandleRaceCommandAsync(SocketCommandContext context, string[] args)
        {
            if (args.Length > 1 && args[1] == RaceUUID)
            {
                string username = context.User.Username;
                await context.Channel.SendMessageAsync($"Asking Pumpkin Splat to a race, Please wait {username}...");

                // Simulate a delay and response
                await Task.Delay(1000); // 1 second
                await context.Channel.SendMessageAsync("Pumpkin Splat has declined the race request, sorry.");
            }
            else
            {
                await context.Channel.SendMessageAsync("Invalid race UUID.");
            }
        }
    }
}
