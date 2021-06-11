using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VkApi.DataEntities
{
    class Post
    {
        [JsonProperty("post_id")]
        public int PostId { get; set; }
    }
}
