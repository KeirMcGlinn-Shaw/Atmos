using Newtonsoft.Json;

namespace AtmosMusicBot
{
    public struct ConfigJson
    {
        [JsonProperty("token")] // Defines which property in config.json should be stored in the following field
        public string Token { get; private set; } // Will hold the token string stored in config.json

        [JsonProperty("prefix")] // Defines which property in config.json should be stored in the following field
        public string Prefix { get; private set; } // Will hold the prefix string stored in config.json

    }
}
