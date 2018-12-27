using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class ImageTypesResponse : ApiResponse
    {
        internal ImageTypesResponse() { }

        [JsonProperty("types")]
        public string[] Types { get; internal set; }

        [JsonProperty("preview")]
        ///<summary>Will be empty if not asked to provide preview in request</summary>
        public PartialImage[] Preview { get; internal set; }
    }
}
