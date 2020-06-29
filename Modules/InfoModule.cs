using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace AtmosMusicBot
{
    // Create a module with no prefix
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

        // ReplyAsync is a method on ModuleBase 
    }

    public class AudioModule : ModuleBase
    {
        [Command("join", RunMode = RunMode.Async)]
        [Summary("Join specified voice channel")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            // Get audio channel
            channel = channel ?? (Context.User as IGuildUser)?.VoiceChannel;

            if (channel == null)
            {
                await Context.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument.");
                return;
            }

            var audioClient = await channel.ConnectAsync();
        }

        [Command("leave", RunMode = RunMode.Async)]
        [Summary("Leave a specified channel")]
        public async Task LeaveChannel()
        {
            var user = scc.User as SocketGuildUser;
            IVoiceChannel channel = user.VoiceChannel;

            if (channel != null)
            {
                await Context.Channel.SendMessageAsync("Atmos has been moved out of " + channel.Name);
                await user.VoiceChannel.DisconnectAsync();     
            }
            else
            {
                await Context.Channel.SendMessageAsync("Atmos is not in a voice channel");
            }
        }
    }
}
