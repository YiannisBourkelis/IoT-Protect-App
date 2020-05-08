using System;
namespace IoTProtect.Models
{
    public class DeviceData
    {
        public DeviceData()
        {
        }

        public long ID { get; set; }
        public long DeviceID { get; set; } //server side check that the Device belongs to the authenticated user
        public DateTime DateCreated { get; set; }
        public int Photoresistor { get; set; }
        public int Flame { get; set; }
        public double Temperature { get; set; }
        public string FirmwareVersion { get; set; }
    }
}
