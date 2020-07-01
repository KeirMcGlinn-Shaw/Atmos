using AtmosMusicBot.Services;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AtmosMusicBot.Modules
{
    /*
     * Module which contains commands which provide audio functionality
     */
    public class AudioModule : ModuleBase
    {
        // Command Atmos to join a voice channel
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

            AudioService.AudioClient = await channel.ConnectAsync();
        }

        // Command Atmos to leave a voice channel
        [Command("leave", RunMode = RunMode.Async)]
        [Summary("Leave a specified channel")]
        public async Task LeaveChannel()
        {
            await AudioService.AudioClient.StopAsync();
        }
    }
}
