using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace VkApi
{
    class GenerateRequestMethod
    {
        private static readonly ResourceManager RequestItems = Resources.RequestItems.ResourceManager;

        private static string wall = RequestItems.GetString("wall");
        private static string photos = RequestItems.GetString("photos");
        private static string likes = RequestItems.GetString("likes");

        private static string post = RequestItems.GetString("post");
        private static string createComment = RequestItems.GetString("createComment");
        private static string edit = RequestItems.GetString("edit");
        private static string delete = RequestItems.GetString("delete");
        private static string savePhoto = RequestItems.GetString("savePhoto");
        private static string getServer = RequestItems.GetString("getServer");
        private static string getList = RequestItems.GetString("getList");

        private static string ownerId = RequestItems.GetString("ownerId");
        private static string accessToken = RequestItems.GetString("accessToken");
        private static string version = RequestItems.GetString("version");
        private static string message = RequestItems.GetString("message");
        private static string postId = RequestItems.GetString("postId");
        private static string attachPhoto = RequestItems.GetString("attachPhoto");
        private static string server = RequestItems.GetString("server");
        private static string photo = RequestItems.GetString("photo");
        private static string hash = RequestItems.GetString("hash");

        public static string CreateWallPostRequest(string versonD, string accessTokenD, string userId, string text)
        {
            return string.Concat(wall, post, version, versonD, accessToken, accessTokenD, ownerId, userId, message, text);
        }

        internal static string CreateWallCreatCommentRequest(string versionD, string accessTokenD, string userId, string comment, string postIdD)
        {
            return string.Concat(wall, createComment, version, versionD, accessToken, accessTokenD, ownerId, userId, message, comment, postId, postIdD);
        }

        internal static string CreateWallEditRequest(string versionD, string accessTokenD, string userId, string postText, string postIdD, JToken owner, JToken id)
        {
            return string.Concat(wall, edit, version, versionD, accessToken, accessTokenD, ownerId, userId, message, postText, postId, postIdD, attachPhoto, owner, "_", id);
        }

        internal static string CreateWallDeleteRequest(string versionD, string accessTokenD, string userId, string postIdD)
        {
            return string.Concat(wall, delete, version, versionD, accessToken, accessTokenD, ownerId, userId, postId, postIdD);
        }

        internal static string CreatePhotoSaveRequest(string versionD, string accessTokenD, JToken serverD, JToken photoD, JToken hashD)
        {
            return string.Concat(photos, savePhoto, version, versionD, accessToken, accessTokenD, server, serverD, photo, photoD, hash, hashD);
        }

        internal static string CreatePhotoGetServerRequest(string versionD, string accessTokenD, string userId)
        {
            return string.Concat(photos, getServer, version, versionD, accessToken, accessTokenD, ownerId, userId);
        }

        internal static string CreateLikesGetListRequest(string versionD, string accessTokenD, string userId, string postIdD)
        {
            return string.Concat(likes, getList, version, versionD, accessToken, accessTokenD, ownerId, userId, postId, postIdD);
        }
    }
}
