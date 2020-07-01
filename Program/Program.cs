using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AtmosMusicBot;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

class Program
{
    private readonly DiscordSocketClient _client;

    private readonly CommandService _commands;

    // Will hold the token key used to connect to the discord socket client
    private string token;
    // Will hold the prefix string used to define commands to this bot
    private string prefix;

    //public DiscordSocketClient Client { get => _client; }

    // Program entry point
    static void Main(string[] args)
    {
        // Call the Program constructor, followed by the 
        // MainAsync method and wait until it finishes (which should be never).
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    private Program()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            // Specify logging level
            LogLevel = LogSeverity.Info,
        });

        _commands = new CommandService(new CommandServiceConfig
        {
            // Again, log level:
            LogLevel = LogSeverity.Info,

            // Remove need for commands to be case sensitive
            CaseSensitiveCommands = false,
        });

        // Subscribe the logging handler to both the client and the CommandService.
        _client.Log += Log;
        _commands.Log += Log;

    }


    // Logging handler
    private static Task Log(LogMessage message)
    {
        switch (message.Severity)
        {
            case LogSeverity.Critical:
            case LogSeverity.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case LogSeverity.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;
            case LogSeverity.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case LogSeverity.Verbose:
            case LogSeverity.Debug:
                Console.ForegroundColor = ConsoleColor.DarkGray;
                break;
        }
        Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
        Console.ResetColor();

        return Task.CompletedTask;
    }



    /*
     * Task which provides an asynchronus context for the variaty of async methods that operate in this program
     * 
     * Essentially a main method for this program but in the form of an async operation
     */
    private async Task MainAsync()
    {
        // Centralize the logic for commands into a separate method.
        await InitCommands();

        // Will hold intial string returned by streamreader that reads config.json
        var json = string.Empty;

        // Read data fron config.json
        using (var fs = File.OpenRead("Services\\config.json"))
        using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            json = await sr.ReadToEndAsync().ConfigureAwait(false);

        // Store the data returned by the StreamReader into a ConfigJson struct
        var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

        // Assign token and prefix to the related field in the struct
        token = configJson.Token;
        prefix = configJson.Prefix;
        
        // Login and connect.
        await _client.LoginAsync(TokenType.Bot, token);

        await _client.StartAsync();

        // Wait infinitely so your bot actually stays connected.
        await Task.Delay(Timeout.Infinite);
    }

    private async Task InitCommands()
    {
        // Search program and add all Module classes that are discovered
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);


        // Subscribe a handler to see if a message invokes a command.
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        // Bail out if it's a System Message.
        var msg = arg as SocketUserMessage;
        if (msg == null) return;

        // Stop bot replying to itself
        if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

        // Create a number to track where the prefix ends and the command begins
        int pos = 0;

        // Checks for commands in the chat (Messages with the command prefix (!) or that mention the bot)
        if (msg.HasCharPrefix(prefix[0], ref pos)  || msg.HasMentionPrefix(_client.CurrentUser, ref pos) )
        {
            // Create a Command Context.
            var context = new SocketCommandContext(_client, msg);

            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully).
            var result = await _commands.ExecuteAsync(context, pos, null);

            // Send message if command fails (does not catch errors from commands with 'RunMode.Async', subscribe a handler for '_commands.CommandExecuted' to see those)
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                await msg.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}