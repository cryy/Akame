using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    internal class UploadImageResponse : ApiResponse
    {
        [JsonProperty("file")]
        internal Image UploadedImage { get; set; }
    }
}
