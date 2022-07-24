using System.Reflection;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordDnDApplication;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly CommandService _commands;
    private readonly IServiceProvider _service;
    
    public CommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider service)
    {
        _commands = commands;
        _client = client;
        _service = service;
    }

    public async Task InstallCommandAsync()
    {
        await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: _service);
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage messageParam)
    {
        Console.WriteLine($"Got text message {messageParam.Content}");
        var message = messageParam as SocketUserMessage;

        if (message == null) return;

        int argPos = 0;
        
        if(!(message.HasCharPrefix('!', ref argPos) || 
             message.HasMentionPrefix(_client.CurrentUser, ref argPos) ||
             message.Author.IsBot)) return;

        var context = new SocketCommandContext(_client, message);

        await _commands.ExecuteAsync(
            context: context,
            argPos: argPos,
            services: _service
        );
    }
}