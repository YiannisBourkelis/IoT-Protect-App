using System;
using System.Collections.Generic;

namespace IoTProtect.Models
{
    public class SimplePaginator<T>
    {
        public SimplePaginator()
        {
        }

        [Newtonsoft.Json.JsonProperty("current_page")]
        public ulong CurrentPage { get; set; }
        [Newtonsoft.Json.JsonProperty("data")]
        public List<T> Data { get; set; }

    }
}

