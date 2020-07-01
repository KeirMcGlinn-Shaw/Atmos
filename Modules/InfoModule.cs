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

    }
}
