using System;
namespace IoTProtect.Models
{
    public class EnvMonStationMeasurement : ITeamChild<EnvMonStationMeasurement>, IMessaging, IRest
    {
        public EnvMonStationMeasurement()
        {
        }

        [Newtonsoft.Json.JsonProperty("id")]
        public ulong ID { get; set; }

        //Συσχέτιση device με τον user
        [Newtonsoft.Json.JsonProperty("team_id")]
        public ulong DeviceID { get; set; }

        [Newtonsoft.Json.JsonProperty("temperature")]
        public decimal Temperature { get; set; }

        [Newtonsoft.Json.JsonProperty("pressure")]
        public decimal Pressure { get; set; }

        [Newtonsoft.Json.JsonProperty("humidity")]
        public decimal Humidity { get; set; }

        //TODO: kapoies fores - spania (peripou mia metrisi se 10 meres) oi times tou PMS einai null
        [Newtonsoft.Json.JsonProperty("PMS7003_MP_2_5")]
        public decimal? PMS7003_MP_2_5 { get; set; }

        [Newtonsoft.Json.JsonProperty("PMS7003_MP_10")]
        public decimal? PMS7003_MP_10 { get; set; }

        [Newtonsoft.Json.JsonProperty("carbonDioxide")]
        public decimal CarbonDioxide { get; set; }

        [Newtonsoft.Json.JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [Newtonsoft.Json.JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        //converters
        [Newtonsoft.Json.JsonIgnore]
        public DateTime CreatedAtLocalDateTime => this.CreatedAt.ToLocalTime();

        //IMessaging interface
        [Newtonsoft.Json.JsonIgnore]
        public string ItemInsertedMessage { get => ""; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemUpdatedMessage { get => ""; }
        [Newtonsoft.Json.JsonIgnore]
        public string ItemDeletedMessage { get => ""; }

        //IRest Interface
        [Newtonsoft.Json.JsonIgnore]
        public string CreateItemRoutePath { get => ""; }
        [Newtonsoft.Json.JsonIgnore]
        public string ReadItemsRoutePath { get => $"/api/device/{DeviceID}/measurements"; }
        [Newtonsoft.Json.JsonIgnore]
        public string UpdateItemRoutePath { get => ""; }
        [Newtonsoft.Json.JsonIgnore]
        public string DeleteItemRoutePath { get => $""; }

        public bool HaveSamePropertyValues(EnvMonStationMeasurement item)
        {
            return this.ID == item.ID &&
                this.DeviceID == item.DeviceID;
        }
    }
}

