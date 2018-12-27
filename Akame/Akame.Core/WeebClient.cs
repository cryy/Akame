using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Akame
{
    public class WeebClient
    {
        internal HttpClient HttpClient;

        /// <summary>Bot version set in <see cref="WeebClient"/>'s constructor. Can be null.</summary>
        public string Version { get; }

        /// <summary>
        /// Creates a new instance of <see cref="WeebClient"/>
        /// </summary>
        /// <param name="token">Your weeb.sh token</param>
        /// <param name="type">Weeb.sh token type to use</param>
        /// <param name="version">Optional bot version to display in requests.</param>
        public WeebClient(string token, TokenType type, string version = null)
        {
            Version = version;
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri("https://api.weeb.sh");
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"{type} {token}");
            if(version != null)
                HttpClient.DefaultRequestHeaders.Add("User-Agent", version);
        }
    }
}
