using AutoItX3Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Net;
using System.Resources;
using System.Text;
using VkApi;
using VkApi.DataEntities;

namespace VkApi
{
    class HTTPUtils
    {
        static IRestClient client;
        static IRestRequest request;
        static IRestResponse response;
        //readonly static ResourceManager resourceManager = confData.ResourceManager;
        AutoItX3 autoIt = new AutoItX3();

        public static void CreateClient()
        {
            client = new RestClient($"https://api.vk.com/method/");
        }


        public static void CreateGetPostsRequest()
        {
            request = new RestRequest($"posts", Method.GET);
        }

        public static void CreatePostPostsRequest()
        {
            request = new RestRequest($"wall.post?owner_id=627657327&message=hi!&access_token=e10dbce942d3f97aa07321717f1880ac3b50db8d883f6af264b63533691394c27b933c13755362fe81209&v=5.131", Method.POST);
        }

        internal static void DownloadPhoto()
        {
            throw new NotImplementedException();
        }

        public static void CreateGetPostsRequestWithParam(string content)
        {
            request = new RestRequest($"posts/{content}", Method.GET);
        }

        internal static void UploadPhoto(string postId)
        {
            var c = new WebClient();
            String u2 = new String("https://pu.vk.com/c516136/ss2254/upload.php?act=do_add&mid=627657327&aid=-14&gid=0&hash=846c8923f4b8db02cbab56cdc4247afe&rhash=4e3207058bfd8eb3d0e0e8771b251469&swfupload=1&api=1&wallphoto=1");
            var r2 = Encoding.UTF8.GetString(c.UploadFile(u2, "POST", "C:\\a.jpg"));

            var j2 = JsonConvert.DeserializeObject(r2) as JObject;
            //
            var u3 = "https://api.vk.com/method/photos.saveWallPhoto?access_token=e10dbce942d3f97aa07321717f1880ac3b50db8d883f6af264b63533691394c27b933c13755362fe81209&v=5.131"
                     + "&server=" + j2["server"]
                     + "&photo=" + j2["photo"]
                     + "&hash=" + j2["hash"];
            var r3 = c.DownloadString(u3);
            var j3 = JsonConvert.DeserializeObject(r3) as JObject;

            var u4 = "https://api.vk.com/method/wall.edit?access_token=e10dbce942d3f97aa07321717f1880ac3b50db8d883f6af264b63533691394c27b933c13755362fe81209&v=5.131"
             + "&owner_id" + j3["response"][0]["owner_id"]
             + "&post_id=" + postId
             + "&message=" + "hi"
             + "&attachments=photo"+ j3["response"][0]["owner_id"] + "_"+ j3["response"][0]["id"];
            var r4 = c.DownloadString(u4);
            //client = new RestClient($"https://api.vk.com/method/");
            //request = new RestRequest($"https://pu.vk.com/c516136/ss2254/upload.php?act=do_add&mid=627657327&aid=-14&gid=0&hash=846c8923f4b8db02cbab56cdc4247afe&rhash=4e3207058bfd8eb3d0e0e8771b251469&swfupload=1&api=1&wallphoto=1", Method.POST);
            //request.AlwaysMultipartFormData = true;

            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AddFile("image", "C:\\a.jpg", "multipart/form-data");
            //response = client.Execute(request);
        }

        public static void CreateGetUsersRequest()
        {
            request = new RestRequest($"users", Method.GET);
        }

        public static void CreateGetUsersRequestWithParam(string content)
        {
            request = new RestRequest($"users/{content}", Method.GET);
        }

        public static T CreateResponse<T>()
        {
            //не знаю или можно здесь упростить, потому что response нужна для других методов
            response = client.Execute(request);
            return JsonConverter.CreateFromJson<T>(response.Content.ToString());
            //return new JsonSerializer().Deserialize<T>(response);
        }

        public static HttpStatusCode GetStatusCode()
        {
            return response.StatusCode;
        }

        public static string GetResponseContentType()
        {
            return response.ContentType;
        }

        public static string GetResponseContent()
        {
            return response.Content;
        }

        public static void AddJsonBodyToRequest<T>(T addedPost)
        {
            //request.AddJsonBody(JsonConvertUtil.SerializeObj(addedPost));
        }
    }
}