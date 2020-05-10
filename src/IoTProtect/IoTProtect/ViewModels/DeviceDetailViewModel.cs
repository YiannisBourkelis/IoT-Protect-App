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

            //check if the device group for the selected device exist
            DeviceInfoList tmpDeviceGroup = null;
            foreach (var deviceGroup in DeviceGroupList)
            {
                if (deviceGroup.Heading == deviceInfo.Location)
                {
                    tmpDeviceGroup = deviceGroup;
                }
            }

            //check if we should move the device in a new group
            //or we need to create a new group
            foreach (var deviceGroup in DeviceGroupList)
            {

                int idx = deviceGroup.IndexOf(deviceInfo);
                if (idx > -1)
                {
                    //device found

                    //if the user changed the device location to an other
                    //device group that exists, then move it to the new  group
                    if (tmpDeviceGroup != null && deviceGroup.Heading != deviceInfo.Location)
                    {
                        deviceGroup.RemoveAt(idx);
                        tmpDeviceGroup.Add(deviceInfo);
                    }
                    else
                    {
                        //if the user changed the device location to a
                        //device group that does not exist, then create the new  group
                        //and move the device to the new group
                        if (tmpDeviceGroup == null)
                        {
                            deviceGroup.RemoveAt(idx);
                            DeviceInfoList newGroup = new DeviceInfoList();
                            newGroup.Heading = deviceInfo.Location;
                            newGroup.Add(deviceInfo);
                            DeviceGroupList.Add(newGroup);
                        }
                    }

                    //if the device group that the device was belonging
                    //does not contain any other devices
                    //remove it
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
