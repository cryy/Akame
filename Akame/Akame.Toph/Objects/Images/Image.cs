using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class Image : PartialImage
    {
        internal Image() { }

        [JsonProperty("nsfw")]
        ///<summary>Whether this image has content that could be considered NSFW (not safe for work)</summary>
        public bool IsNsfw { get; internal set; }

        [JsonProperty("tags")]
        ///<summary>Tags associated with this image</summary>
        public string[] Tags { get; internal set; }

        [JsonProperty("hidden")]
        ///<summary>Whether this image can only be seen by the uploader</summary>
        public bool IsHidden { get; internal set; }

        [JsonProperty("source")]
        ///<summary>Source url of the image, can be null</summary>
        public string SourceUrl { get; internal set; }

        [JsonProperty("account")]
        ///<summary>Id of the account that uploaded that image</summary>
        public string UploaderId { get; internal set; }
    }
}
