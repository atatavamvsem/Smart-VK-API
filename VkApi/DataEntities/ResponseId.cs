using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace VkApi.DataEntities
{
    class ResponseId
    {
        [JsonProperty("response")]
        public Post Response { get; set; }
    }
}
