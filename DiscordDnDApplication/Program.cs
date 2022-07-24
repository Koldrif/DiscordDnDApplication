// See https://aka.ms/new-console-template for more information

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordDnDApplication;
public class Program
{
    private DiscordSocketClient _client;

    private CommandService _command;

    private LoggingService _log;
    
    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();

        _command = new CommandService();

        _log = new LoggingService(_client, _command);

        _client.Log += Log;

        _client.MessageReceived += OnMsgRecieved;
        
        var token = "MTAwMDEzNTU2NjQyNTMyNTcxOA.G_6g9k.S-keFlqK0C5-M0DZNvKGHkjZN6rFExcYNbn4XA";

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }
// Token MTAwMDEzNTU2NjQyNTMyNTcxOA.G_6g9k.S-keFlqK0C5-M0DZNvKGHkjZN6rFExcYNbn4XA
    private Task Log(LogMessage msg)
    {
        Console.WriteLine($"Log message:\n{msg.Message}");
        return Task.CompletedTask;
    }

    private Task OnMsgRecieved(SocketMessage msg)
    {
        Console.WriteLine($"{msg.Author} send message into: {msg.Channel}\nText: {msg.Content}");
        return Task.CompletedTask;;
    }
    
}