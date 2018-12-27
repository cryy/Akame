using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame
{
    public class ApiResponse
    {
        internal ApiResponse() { }

        [JsonProperty("status")]
        internal int StatusCode { get; set; }

        [JsonProperty("message")]
        internal string ErrorMessage { get; set; }
    }
}
