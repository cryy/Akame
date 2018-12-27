using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akame.Toph
{
    public class ImageUploadSettings
    {
        public string Type { get; }
        public bool Hidden { get; }
        public string[] Tags { get; }
        public bool Nsfw { get; }
        public string Source { get; }

        public ImageUploadSettings(string Type, bool Hidden = false, string[] Tags = null, bool Nsfw = false, string Source = "")
        {
            this.Type = Type;
            this.Hidden = Hidden;
            this.Tags = Tags;
            this.Nsfw = Nsfw;
            this.Source = Source;
        }

        internal string Serialize(string url)
        {
            var jobj = new JObject();

            jobj.Add(new JProperty("url", url));
            jobj.Add(new JProperty("baseType", Type));
            jobj.Add(new JProperty("hidden", Hidden));
            jobj.Add(new JProperty("tags", Tags == null ? "" : string.Join(",", Tags)));
            jobj.Add(new JProperty("nsfw", Nsfw));
            jobj.Add(new JProperty("source", Source));

            return jobj.ToString();
        }
    }
}
