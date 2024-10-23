using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using toonbot.commands;
using toonbot.net;

namespace toonbot.main
{
    class Bot
    {
        private DiscordSocketClient _client;
        private const string Token = "YOUR_DISCORD_TOKEN_HERE";

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.Guilds | GatewayIntents.GuildMessages | GatewayIntents.MessageContent
            });

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;
            _client.MessageReceived += MessageReceivedAsync;

            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();

            await Task.Delay(-1);  // Keep the bot running
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log);
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Logged in as {_client.CurrentUser}");
            _client.SetActivityAsync(new Game("Cog Invasion", ActivityType.Playing));
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message is not SocketUserMessage userMessage) return;

            int argPos = 0;
            if (!(userMessage.HasCharPrefix('!', ref argPos) || userMessage.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            var context = new SocketCommandContext(_client, userMessage);

            // Command handling
            await CommandHandler.HandleCommandAsync(context);
        }
    }
}
