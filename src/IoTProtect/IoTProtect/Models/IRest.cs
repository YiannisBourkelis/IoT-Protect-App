using System;
namespace IoTProtect.Models
{
    public interface IRest
    {
        public string CreateItemRoutePath { get; }
        public string ReadItemsRoutePath { get; }
        public string UpdateItemRoutePath { get; }
        public string DeleteItemRoutePath { get; }
    }
}

