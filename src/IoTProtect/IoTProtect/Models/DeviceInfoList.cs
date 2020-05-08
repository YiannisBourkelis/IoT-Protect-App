using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace IoTProtect.Models
{
    public class DeviceInfoList : ObservableCollection<DeviceInfo>, INotifyPropertyChanged
    {
        //public events
        //public event PropertyChangedEventHandler PropertyChanged;

        //public properties
        public string mHeading;
        public string Heading {
            get { return mHeading; }
            set {
                mHeading = value;
                PropertyChangedEventArgs p = new PropertyChangedEventArgs("Heading");
                base.OnPropertyChanged(p);
                //PropertyChanged?.Invoke(this, p);
            }
        }

        public string IndexName { get; set; }

        public DeviceInfoList()
        {
            IndexName = "A";
        }

    }
}
