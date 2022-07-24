// See https://aka.ms/new-console-template for more information

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace DiscordDnDApplication;
public class Program
{
    private DiscordSocketClient _client;

    private CommandService _command;

    private LoggingService _log;

    private CommandHandler _handler;

    private IServiceProvider _service;
    
    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();

        _command = new CommandService();

        _log = new LoggingService(_client, _command);

        _service = new ServiceCollection()
            .AddSingleton(_client)
            .AddSingleton(_command)
            .AddSingleton<InfoModule>()
            .AddSingleton<SampleModule>()
            .AddSingleton<VoiceModule>()
            .BuildServiceProvider();

        _handler = new CommandHandler(_client, _command, _service);

        await _handler.InstallCommandsAsync();
        
        var token = await (new StreamReader("token.private")).ReadToEndAsync();

        _client.Ready += CreateSlashCommandAsync;

        await _client.LoginAsync(TokenType.Bot, token);

        await _client.StartAsync();
        

        

        await Task.Delay(-1);
    }

    public async Task CreateSlashCommandAsync()
    {
    //     var guild = _client.GetGuild();
    //
    // // Next, lets create our slash command builder. This is like the embed builder but for slash commands.
    // var guildCommand = new SlashCommandBuilder();
    //
    // // Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$
    // guildCommand.WithName("first-command");
    //
    // // Descriptions can have a max length of 100.
    // guildCommand.WithDescription("This is my first guild slash command!");

    // Let's do our global command
    var globalCommand = new SlashCommandBuilder();
    globalCommand.WithName("first-global-command");
    globalCommand.WithDescription("This is my first global slash command");


        // Now that we have our builder, we can call the CreateApplicationCommandAsync method to make our slash command.
        // await guild.CreateApplicationCommandAsync(guildCommand.Build());

        // With global commands we don't need the guild.
        await _client.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        // Using the ready event is a simple implementation for the sake of the example. Suitable for testing and development.
        // For a production bot, it is recommended to only run the CreateGlobalApplicationCommandAsync() once for each command.
    }

}