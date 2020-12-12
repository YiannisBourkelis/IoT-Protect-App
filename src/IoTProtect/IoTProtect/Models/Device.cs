using System;
namespace IoTProtect.Models
{
    public class Device : ITeam<Device>, IMessaging, IRest
    {
        public Device()
        {
        }

        //ITeam Interface
        [Newtonsoft.Json.JsonProperty("id")]
        public ulong ID { get; set; }
        [Newtonsoft.Json.JsonProperty("user_id")]
        public ulong UserID { get; set; }
        [Newtonsoft.Json.JsonProperty("name")]
        public string Name { get; set; }
        [Newtonsoft.Json.JsonProperty("personal_team")]
        public bool PersonalTeam { get; set; }

        //device
        [Newtonsoft.Json.JsonProperty("place")]
        public string Place { get; set; }
        [Newtonsoft.Json.JsonProperty("location")]
        public string Location { get; set; }
        [Newtonsoft.Json.JsonProperty("type")]
        public DeviceModelsEnum Type { get; set; }

        //last measurement
        [Newtonsoft.Json.JsonProperty("latest_smoke_detector_measurement")]
        public SmokeDetectorMeasurement LatestMeasurement { get; set; }

        //IMessaging interface
        [Newtonsoft.Json.JsonIgnore]
        public string ItemInsertedMessage { get => "DeviceInserted"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemUpdatedMessage { get => "DeviceUpdated"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemDeletedMessage { get => "DeviceDeleted"; }

        //IRest Interface
        [Newtonsoft.Json.JsonIgnore]
        public string CreateItemRoutePath { get => "/api/company/series"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ReadItemsRoutePath { get => $"/api/user/devices"; }
        [Newtonsoft.Json.JsonIgnore]
        public string UpdateItemRoutePath { get => "/api/company/series"; }
        [Newtonsoft.Json.JsonIgnore]
        public string DeleteItemRoutePath { get => $"/api/series/{this.ID}"; }


        public bool HaveSamePropertyValues(Device item)
        {
            return this.ID == item.ID &&
                this.UserID == item.UserID;
        }
    }
}
