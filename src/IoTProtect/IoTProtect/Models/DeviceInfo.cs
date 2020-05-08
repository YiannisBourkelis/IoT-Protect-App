using System;
using IoTProtect.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace IoTProtect.Models
{
    public class DeviceInfo : INotifyPropertyChanged
    {
        //public events
        public event PropertyChangedEventHandler PropertyChanged;

        public DeviceInfo()
        {
        }

        public long ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        private string mDescription;
        public string Description {
                get
                {
                    return mDescription;
                }
                set
                {
                    mDescription = value;
                    PropertyChangedEventArgs p = new PropertyChangedEventArgs("Description");
                    PropertyChanged?.Invoke(this, p);
                }
            }
        // Kitchen Smoke Sensor
        public string Location { get; set; } //where the device is located e.g. Home
        public DeviceModelsEnum Model { get; set; }

        public List<DeviceData> SensorsData { get; set; }

    }
}
