using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class RandomImageResponse : ApiResponse
    {
        internal RandomImageResponse() { }

        [JsonProperty("tags")]
        public Image Image { get; internal set; }
    }
}
