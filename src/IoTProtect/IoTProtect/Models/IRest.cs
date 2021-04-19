using System;
namespace IoTProtect.Models
{
    public interface IRest
    {
        public string CreateItemRoutePath { get; }
        public string ReadItemsRoutePath { get; }
        public string ReadItemsNextPageUrl { get; set; }
        public string UpdateItemRoutePath { get; }
        public string DeleteItemRoutePath { get; }
    }
}

