using Discord.Commands;
using System.Threading.Tasks;
using toonbot.commands;

namespace toonbot.main
{
    public static class CommandHandler
    {
        public static async Task HandleCommandAsync(SocketCommandContext context)
        {
            var message = context.Message;
            var args = message.Content.Split(' ');

            // Example: Use API-based method for one instance
            bool useApi = (context.Guild?.Name == "API-Based Server");

            switch (args[0])
            {
                case "!toonlink":
                    await ToonLinkCommand.HandleToonLinkAsync(context, args, useApi);
                    break;

                // Handle other commands here...

                default:
                    break;
            }
        }
    }
}
