using Discord.Commands;
using System.Threading.Tasks;
using toonbot.net;

namespace toonbot.commands
{
    public static class ToonLinkCommand
    {
        private const string ValidUUID = "7a66d948-0ee7-4300-9b31-75d69fd9dbc6";

        public static async Task HandleToonLinkAsync(SocketCommandContext context, string[] args, bool useApi = false)
        {
            string username = context.User.Username;

            if (args.Length > 1)
            {
                string response;
                if (useApi)
                {
                    response = await ApiAccountLinking.LinkAccountAsync(username, args[1]);
                }
                else
                {
                    response = await OriginalAccountLinking.LinkAccountAsync(username, ValidUUID, args[1]);
                }

                await context.Channel.SendMessageAsync(response);
            }
            else
            {
                await context.Channel.SendMessageAsync("Please provide a UUID.");
            }
        }
    }
}
