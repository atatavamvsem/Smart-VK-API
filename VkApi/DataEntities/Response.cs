using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VkApi.DataEntities
{
    class Response
    {
        [JsonProperty("response")]
        public Post Post { get; set; }
    }
}
