using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class ImageTagsResponse : ApiResponse
    {
        internal ImageTagsResponse() { }

        [JsonProperty("tags")]
        public string[] Tags { get; internal set; }
    }
}
