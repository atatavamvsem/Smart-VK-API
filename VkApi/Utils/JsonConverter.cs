using Newtonsoft.Json;
using System.IO;

namespace VkApi
{
    class JsonConverter
    {
        public static T CreateFromJson<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(path);
        }

        public static string SerializeObj<T>(T addedPost)
        {
            return JsonConvert.SerializeObject(addedPost);
        }
    }
}