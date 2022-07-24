using Discord;
using Discord.Commands;

namespace DiscordDnDApplication;

public class VoiceModule : ModuleBase<SocketCommandContext>
{
    [Command("join")]
    [Summary("Bot will join channel where user, or join chat with given name")]
    public async Task JoinChannel(IVoiceChannel channel = null)
    {
        channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;
        if (channel == null)
        {
            await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); 
            return;
        }

        var audioClient = await channel.ConnectAsync();
    }
}