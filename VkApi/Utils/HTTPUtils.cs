using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;

namespace VkApi
{
    class HTTPUtils
    {
        private static readonly ResourceManager TestData = Resources.TestData.ResourceManager;
        private static string userId = TestData.GetString("userId");
        private static string accessToken = TestData.GetString("accessToken");
        private static string version = TestData.GetString("version");

        static IRestClient client;
        static IRestRequest request;
        static IRestResponse response;
        static JObject responseJ;
        //readonly static ResourceManager resourceManager = confData.ResourceManager;

        public static void CreateClient()
        {
            client = new RestClient(TestData.GetString("apiUrl"));
        }

        public static string CreatePost(string postText)
        {
            request = new RestRequest($"wall.post?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&owner_id=" + userId
                + "&message=" + postText, Method.POST);
            //response = client.Execute(request);
            responseJ = JsonConvert.DeserializeObject(client.Execute(request).Content.ToString()) as JObject;
            return (string)responseJ["response"]["post_id"];
        }

        public static void CreateComment(string comment, string postId)
        {
            request = new RestRequest($"wall.createComment?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&owner_id=" + userId
                + "&message=" + comment
                + "&post_id=" + postId, Method.POST);
            client.Execute(request);
        }

        internal static void DownloadPhoto(string url)
        {
            var fileBytes = client.DownloadData(new RestRequest(url, Method.GET));
            File.WriteAllBytes(Path.Combine(TestData.GetString("pathDownload"), TestData.GetString("nameImageDownload")), fileBytes);
        }

        internal static void ChangePost(string postText,string postId)
        {
            responseJ = GetUploadServer();
            responseJ = UploadPhoto(responseJ);
            responseJ = SaveWallPhoto(responseJ);
            EditPost(responseJ, postId, postText);
        }

        private static void EditPost(JObject responseJ, string postId, string postText)
        {
            request = new RestRequest($"wall.edit?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&owner_id=" + userId
                + "&post_id=" + postId
                + "&message=" + postText
                + "&attachments=photo" + responseJ["response"][0]["owner_id"] 
                + "_" + responseJ["response"][0]["id"], Method.GET);
            client.Execute(request);
        }

        internal static void DeleatePost(string postId)
        {
            request = new RestRequest($"wall.delete?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&owner_id=" + userId
                + "&post_id=" + postId, Method.GET);
            response = client.Execute(request);
        }

        private static JObject SaveWallPhoto(JObject responseJ)
        {
            request = new RestRequest($"photos.saveWallPhoto?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&server=" + responseJ["server"]
                + "&photo=" + responseJ["photo"]
                + "&hash=" + responseJ["hash"], Method.POST);
            return JsonConvert.DeserializeObject(client.Execute(request).Content.ToString()) as JObject;
        }

        private static JObject UploadPhoto(JObject responseJ)
        {
            var webClient = new WebClient();
            var response = Encoding.UTF8.GetString(webClient.UploadFile((string)responseJ["response"]["upload_url"], "POST", TestData.GetString("imageUpload")));
            return JsonConvert.DeserializeObject(response) as JObject;
        }

        private static JObject GetUploadServer()
        {
            request = new RestRequest($"photos.getWallUploadServer?"
                + "v=" + version
                + "&access_token=" + accessToken
                + "&owner_id=" + userId, Method.POST);
            response = client.Execute(request);
            return JsonConvert.DeserializeObject(response.Content.ToString()) as JObject;
        }

        public static string GetLikes(string postId)
        {
            request = new RestRequest($"likes.getList?"
                + "v=" + version
                + "&type=post"
                + "&access_token=" + accessToken
                + "&owner_id=" + userId
                + "&item_id=" + postId, Method.GET);
            responseJ = JsonConvert.DeserializeObject(client.Execute(request).Content.ToString()) as JObject;
            return (string)responseJ["response"]["items"][0];
        }
    }
}