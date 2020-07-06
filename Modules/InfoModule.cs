using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AtmosMusicBot.Services;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;

namespace AtmosMusicBot
{

    /*
     * Module that contains commands which return information about the bot
     */
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        // !say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

        // TODO Complete command
        //[Command("commands")]
        //[Summary("Lists all of Atmos' commands.")]
        //public async Task ListCommands()
        //{
        //    string commands;

        //    var commandInfo = new StringBuilder();

        //    commandInfo.Append();


        //    await ReplyAsync(commands);
        //}

    }
}
