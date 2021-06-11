using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;
using System.Resources;
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

        public static void CreateGetPostsRequestWithParam(string content)
        {
            request = new RestRequest($"posts/{content}", Method.GET);
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
            ResponseId ff = JsonConverter.CreateFromJson<ResponseId>(response.Content.ToString());
            return new JsonSerializer().Deserialize<T>(response);
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