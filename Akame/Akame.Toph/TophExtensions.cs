using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Akame.Toph
{
    public static class TophExtensions
    {
        /// <summary>
        /// Uploads an image to weeb.sh's CDN. Returns <see cref="Image"/> if successful.
        /// </summary>
        /// <param name="Url">Direct image URL to upload</param>
        public static async Task<Image> UploadImageAsync(this WeebClient client, string Url, ImageUploadSettings Settings)
        {
            if (Url == null || Settings == null)
                throw new ArgumentException("Parameter(s) may not be null.");

            var content = new StringContent(Settings.Serialize(Url), Encoding.UTF8, "application/json");

            var response = await client.HttpClient.PostAsync("/images/upload", content);


            if (!response.IsSuccessStatusCode)
                throw new WeebApiException($"/images/upload responded with \"{response.StatusCode}\".");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UploadImageResponse>(responseContent).UploadedImage;
        }

        /// <summary>
        /// Uploads an image to weeb.sh's CDN. Returns <see cref="Image"/> if successful.
        /// </summary>
        /// <param name="ImageStream">Image stream to upload</param>
        public static async Task<Image> UploadImageAsync(this WeebClient client, Stream ImageStream, ImageUploadSettings Settings)
        {
            if (ImageStream == null || Settings == null)
                throw new ArgumentException("Argument(s) may not be null.");

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(ImageStream), "file");
            content.Add(new StringContent(Settings.Type), "baseType");
            content.Add(new StringContent(Settings.Hidden.ToString()), "hidden");
            content.Add(new StringContent(Settings.Tags == null ? "" : string.Join(",", Settings.Tags)), "tags");
            content.Add(new StringContent(Settings.Nsfw.ToString()), "nsfw");
            content.Add(new StringContent(Settings.Source), "source");

            var response = await client.HttpClient.PostAsync("/images/upload", content);

            if (!response.IsSuccessStatusCode)
                throw new WeebApiException($"/images/upload responded with \"{response.StatusCode}\".");

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<UploadImageResponse>(responseContent).UploadedImage;
        }

        /// <summary>
        /// Returns all possible image types.
        /// </summary>
        /// <param name="Hidden">If true, you only get hidden images you uploaded</param>
        /// <param name="Nsfw">When <see cref="NsfwType.None"/>, no types from nsfw images will be returned, <see cref="NsfwType.Mixed"/> returns types from nsfw and non-nsfw images, <see cref="NsfwType.Only"/> returns only types from nsfw images</param>
        /// <param name="Preview">Defines whether the response should contain a preview for each image type</param>
        public static async Task<ImageTypesResponse> GetImageTypesAsync(this WeebClient client, bool Hidden = false, NsfwType Nsfw = NsfwType.None, bool Preview = false)
        {
            string parameters = $"?nsfw={Nsfw.GetApiName()}";

            if (Hidden)
                parameters += "&hidden=true";
            if (Preview)
                parameters += "&preview=true";

            var response = await client.HttpClient.GetStringAsync($"/images/types{parameters}");

            var realResponse = JsonConvert.DeserializeObject<ImageTypesResponse>(response);

            if (realResponse.StatusCode != 200)
                throw new WeebApiException($"/images/types responded with \"{realResponse.ErrorMessage}\".");

            return realResponse;
        }

        /// <summary>
        /// Returns all possible image tags.
        /// </summary>
        /// <param name="Hidden">If true, you only get hidden images you uploaded</param>
        /// <param name="Nsfw">When <see cref="NsfwType.None"/>, no types from nsfw images will be returned, <see cref="NsfwType.Mixed"/> returns types from nsfw and non-nsfw images, <see cref="NsfwType.Only"/> returns only types from nsfw images</param>
        public static async Task<ImageTagsResponse> GetImageTagsAsync(this WeebClient client, bool Hidden = false, NsfwType Nsfw = NsfwType.None)
        {
            string parameters = $"?nsfw={Nsfw.GetApiName()}";

            if (Hidden)
                parameters += "&hidden=true";

            var response = await client.HttpClient.GetStringAsync($"/images/tags{parameters}");

            var realResponse = JsonConvert.DeserializeObject<ImageTagsResponse>(response);

            if (realResponse.StatusCode != 200)
                throw new WeebApiException($"/images/types responded with \"{realResponse.ErrorMessage}\".");

            return realResponse;
        }

        /// <summary>
        /// Returns a random image from the api. Either <paramref name="Type"/> or <paramref name="Tags"/> must be set.
        /// </summary>
        /// <param name="Type">Type of image you want to get</param>
        /// <param name="Tags">List of tags that the image should have</param>
        /// <param name="Nsfw">When <see cref="NsfwType.None"/>, no types from nsfw images will be returned, <see cref="NsfwType.Mixed"/> returns types from nsfw and non-nsfw images, <see cref="NsfwType.Only"/> returns only types from nsfw images</param>
        /// <param name="Hidden">If true, you only get hidden images you uploaded</param>
        /// <param name="FileType">File type of the image</param>
        public static async Task<Image> GetRandomImageAsync(this WeebClient client, string Type = null, string[] Tags = null, NsfwType Nsfw = NsfwType.None, bool Hidden = false, FileType FileType = FileType.Any)
        {
            if (Type == null && Tags == null)
                throw new ArgumentException("Either type or tags must be provided.");


            string parameters = $"?nsfw={Nsfw.GetApiName()}";

            if (Type != null)
                parameters += $"&type={Type}";
            if (Tags != null)
                parameters += $"&tags={string.Join(",", Tags)}";
            if (Hidden)
                parameters += "&hidden=true";
            if (FileType != FileType.Any)
                parameters += $"&filetype={FileType.ToString().ToLower()}";

            var response = await client.HttpClient.GetStringAsync($"/images/random{parameters}");

            var statusResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

            if (statusResponse.ErrorMessage != null)
                throw new WeebApiException($"/images/random responded with \"{statusResponse.ErrorMessage}\".");

            return JsonConvert.DeserializeObject<Image>(response);
        }

        /// <summary>
        /// Get an image object from an id.
        /// </summary>
        /// <param name="Id">Image id</param>
        public static async Task<Image> GetImageAsync(this WeebClient client, string Id)
        {
            if (Id == null)
                throw new ArgumentException("Id mustn't be null.");

            var response = await client.HttpClient.GetStringAsync($"/info/{Id}");

            var statusResponse = JsonConvert.DeserializeObject<ApiResponse>(response);

            if (statusResponse.ErrorMessage != null)
                throw new WeebApiException($"/info/{Id} responded with \"{statusResponse.ErrorMessage}\".");

            return JsonConvert.DeserializeObject<Image>(response);
        }

        /// <summary>
        /// Delete specified image from the list.
        /// </summary>
        /// <param name="Id">Image id</param>
        public static async Task DeleteImageAsync(this WeebClient client, string Id)
        {
            if (Id == null)
                throw new ArgumentException("Id mustn't be null.");

            var response = await client.HttpClient.DeleteAsync($"/info/{Id}");

            if (!response.IsSuccessStatusCode)
                throw new WeebApiException($"/info/{Id} responded with \"{response.StatusCode}\".");
        }



    }
}
