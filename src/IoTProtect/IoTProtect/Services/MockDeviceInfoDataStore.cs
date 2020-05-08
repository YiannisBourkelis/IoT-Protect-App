using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTProtect.Models;

namespace IoTProtect.Services
{
    public class MockDeviceInfoDatastore : IDataStore<DeviceInfo>
    {
        readonly List<DeviceInfo> devices;

        public MockDeviceInfoDatastore()
        {
            devices = new List<DeviceInfo>()
            {
                new DeviceInfo { ID = 1, Location = "Home",  Description="Kichen Smoke Sensor" },
                new DeviceInfo { ID = 2, Location = "Home",  Description="Living room Smoke Sensor" }
            };
        }

        public async Task<bool> AddItemAsync(DeviceInfo item)
        {
            devices.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(DeviceInfo item)
        {
            var oldItem = devices.Where((DeviceInfo arg) => arg.ID == item.ID).FirstOrDefault();
            devices.Remove(oldItem);
            devices.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = devices.Where((DeviceInfo arg) => arg.ID.ToString() == id).FirstOrDefault();
            devices.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<DeviceInfo> GetItemAsync(string id)
        {
            return await Task.FromResult(devices.FirstOrDefault(s => s.ID.ToString() == id));
        }

        public async Task<IEnumerable<DeviceInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            //await Task.Delay(5000);
            return await Task.FromResult(devices);
        }
    }
}