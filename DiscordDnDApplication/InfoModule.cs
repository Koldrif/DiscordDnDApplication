using Discord.Commands;
using Discord.WebSocket;

namespace DiscordDnDApplication;

public class InfoModule : ModuleBase<SocketCommandContext>
{
    //~say hello world -> Hello world
    [Command("say")]
    [Summary("Echoes a message")]
    public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
    {
        Console.WriteLine("Trying to echo...");
        return ReplyAsync(echo);
    }
}

// Create module with sample prefix
[Group("sample")]
public class SampleModule : ModuleBase<SocketCommandContext>
{
    [Command("square")]
    [Summary("Squares a number")]
    public async Task SquareAsync([Summary("The number to square")] int num)
    {
        await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
    }
    
    [Command("userinfo")]
    [Summary("Returns info about current user? or about user in parameter")]
    [Alias("user", "whois")]
    public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
    {
        var userInfo = user ?? Context.Client.CurrentUser;
        await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
    }
} 