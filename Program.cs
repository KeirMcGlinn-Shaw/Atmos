using Discord;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace AtmosMusicBot
{
    class Program
    {
        /*
         * Program entry point
         * 
         *  Main method synchronus
         */
        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        // Used to connect to Discord
        private DiscordSocketClient _client;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            // Need to move this and store it securely
            var token = "NzI1NDA3ODY5OTA5OTkxNDU1.XvOmVw.A2uW0BEHdz22FpmbbHOO3wVwYgM";

            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            // Block this task until the program is closed
            await Task.Delay(-1);
        }

        /* 
         * Logging method
         * 
         * Logs to Console
         */
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}
