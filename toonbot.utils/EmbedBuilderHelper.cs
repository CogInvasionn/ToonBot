using Discord;

namespace toonbot.utils
{
    public static class EmbedBuilderHelper
    {
        public static Embed BuildEmbed(string title, string description, uint color = 0x3498db)
        {
            return new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(new Color(color))
                .WithTimestamp(System.DateTimeOffset.Now)
                .Build();
        }
    }
}
