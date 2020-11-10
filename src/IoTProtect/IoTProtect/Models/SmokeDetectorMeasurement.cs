using System;
namespace IoTProtect.Models
{
    public class SmokeDetectorMeasurement : ITeamChild<SmokeDetectorMeasurement>, IMessaging, IRest
    {
        public SmokeDetectorMeasurement()
        {
        }

        [Newtonsoft.Json.JsonProperty("id")]
        public ulong ID { get; set; }

        //Συσχέτιση device με τον user
        [Newtonsoft.Json.JsonProperty("team_id")]
        public ulong DeviceID { get; set; }

        [Newtonsoft.Json.JsonProperty("photoresistor")]
        public int Photoresistor { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        //IMessaging interface
        [Newtonsoft.Json.JsonIgnore]
        public string ItemInsertedMessage { get => "SmokeDetectorMeasurementInserted"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemUpdatedMessage { get => "SmokeDetectorMeasurementUpdated"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemDeletedMessage { get => "SmokeDetectorMeasurementDeleted"; }

        //IRest Interface
        [Newtonsoft.Json.JsonIgnore]
        public string CreateItemRoutePath { get => "/api/company/series"; }
        [Newtonsoft.Json.JsonIgnore]
        public string ReadItemsRoutePath { get => $"/api/device/{DeviceID}/measurements"; }
        [Newtonsoft.Json.JsonIgnore]
        public string UpdateItemRoutePath { get => "/api/company/series"; }
        [Newtonsoft.Json.JsonIgnore]
        public string DeleteItemRoutePath { get => $"/api/series/{this.ID}"; }

        public bool HaveSamePropertyValues(SmokeDetectorMeasurement item)
        {
            return this.ID == item.ID &&
                this.DeviceID == item.DeviceID;
        }
    }
}
