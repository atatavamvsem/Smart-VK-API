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

        public static void CreateRestClient(string apiUrl)
        {
            client = new RestClient(apiUrl);
        }

        public static string CreatePost(string postText)
        {
            request = new RestRequest(GenerateRequestMethod.CreateWallPostRequest(version, accessToken, userId, postText), Method.POST);
            responseJ = JsonConvert.DeserializeObject(client.Execute(request).Content.ToString()) as JObject;
            return (string)responseJ["response"]["post_id"];
        }

        public static void CreateComment(string comment, string postId)
        {
            request = new RestRequest(GenerateRequestMethod.CreateWallCreatCommentRequest(version, accessToken, userId, comment, postId), Method.POST);
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
            request = new RestRequest(GenerateRequestMethod.CreateWallEditRequest(version, accessToken, userId, postText, postId, responseJ["response"][0]["owner_id"], responseJ["response"][0]["id"]), Method.GET);
            client.Execute(request);
        }

        internal static void DeleatePost(string postId)
        {
            request = new RestRequest(GenerateRequestMethod.CreateWallDeleteRequest(version, accessToken, userId, postId), Method.GET);
            response = client.Execute(request);
        }

        private static JObject SaveWallPhoto(JObject responseJ)
        {
            request = new RestRequest(GenerateRequestMethod.CreatePhotoSaveRequest(version, accessToken, responseJ["server"], responseJ["photo"], responseJ["hash"]), Method.POST);
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
            request = new RestRequest(GenerateRequestMethod.CreatePhotoGetServerRequest(version, accessToken, userId), Method.POST);
            response = client.Execute(request);
            return JsonConvert.DeserializeObject(response.Content.ToString()) as JObject;
        }

        public static string GetLikes(string postId)
        {
            request = new RestRequest(GenerateRequestMethod.CreateLikesGetListRequest(version, accessToken, userId, postId), Method.GET);
            responseJ = JsonConvert.DeserializeObject(client.Execute(request).Content.ToString()) as JObject;
            return (string)responseJ["response"]["items"][0];
        }
    }
}