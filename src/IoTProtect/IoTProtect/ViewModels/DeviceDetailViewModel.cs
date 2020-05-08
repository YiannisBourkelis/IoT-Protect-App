using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using IoTProtect.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;

namespace IoTProtect.ViewModels
{
    public class DeviceDetailViewModel : BaseViewModel
    {
        public DeviceInfo deviceInfo { get; set; }
        public DeviceInfo deviceInfoModified { get; set; }

        public ObservableCollection<DeviceInfoList> DeviceGroupList { get; set; }


        //public commands
        public Command DeleteDeviceCommand { get; set; }
        public Command UpdateDeviceCommand { get; set; }

        public DeviceDetailViewModel(DeviceInfo device_info)
        {
            deviceInfo = device_info;

            //passing data to the page controls in another device object
            //so that in case the users modifies something and cancels
            //no changes are reflected in the IoTDevices ListView
            deviceInfoModified = new DeviceInfo();
            deviceInfoModified.Location = device_info.Location;
            deviceInfoModified.Description = deviceInfo.Description;

            UpdateDeviceCommand = new Command(async () => await ExecuteUpdateDeviceCommand());
            DeleteDeviceCommand = new Command(async () => await ExecuteDeleteDeviceCommand());
        }

        public DeviceDetailViewModel()
        {
        }

        public async Task<bool> ExecuteUpdateDeviceCommand()
        {
            deviceInfo.Description = deviceInfoModified.Description;
            deviceInfo.Location = deviceInfoModified.Location;



            await Application.Current.MainPage.Navigation.PopModalAsync(true);

            return await Task.FromResult(false);
        }

            public async Task<bool> ExecuteDeleteDeviceCommand()
        {
            //MessagingCenter.Send(this, "DeleteDevice", deviceInfo);
            
            foreach (var deviceGroup in DeviceGroupList)
            {

                int idx = deviceGroup.IndexOf(deviceInfo);
                if (idx > -1)
                {
                    //device found, remove it
                    deviceGroup.RemoveAt(idx);

                    //if after device deletion the group does not
                    //contain any other device, remove the group from the
                    //groups collection, so that no group header appears in listview
                    if (deviceGroup.Count == 0)
                    {
                        DeviceGroupList.Remove(deviceGroup);
                    }

                    await Application.Current.MainPage.Navigation.PopModalAsync(true);
                    return await Task.FromResult(true);
                }
            }
            

            return await Task.FromResult(false);
        }
    }
}
