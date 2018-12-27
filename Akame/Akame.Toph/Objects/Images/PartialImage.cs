using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class PartialImage
    {
        internal PartialImage() { }

        [JsonProperty("url")]
        ///<summary>Full url used to load the image</summary>
        public string Url { get; internal set; }

        [JsonProperty("id")]
        ///<summary>Unique id of the image</summary>
        public string Id { get; internal set; }

        [JsonProperty("fileType")]
        ///<summary>File extension of the image</summary>
        public FileType FileType { get; internal set; }

        [JsonProperty("type")]
        ///<summary>Type/category of the image</summary>
        public string Type { get; internal set; }

    }
}
