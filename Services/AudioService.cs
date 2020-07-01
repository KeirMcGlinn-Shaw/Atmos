using Discord.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace AtmosMusicBot.Services
{
    // Services for Atmos' audio commands
    class AudioService
    {
        private static IAudioClient audioClient;
        // Used by audio commands to control a connection to a voice channel
        public static IAudioClient AudioClient { get => audioClient; set => audioClient = value; }
    }
}
