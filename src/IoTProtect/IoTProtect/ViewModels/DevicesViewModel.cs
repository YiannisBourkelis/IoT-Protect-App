using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using IoTProtect.Models;
using IoTProtect.Views;
using Xamarin.Forms;

namespace IoTProtect.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
        //public properties
        public ObservableCollection<DeviceInfo> Devices { get; set; }
        public ObservableCollection<DeviceInfoList> DevicesListContainer { get; set; }

        //public commands
        public Command LoadDevicesCommand { get; set; }

        public DevicesViewModel()
        {
            Devices = new ObservableCollection<DeviceInfo>();
            DevicesListContainer = new ObservableCollection<DeviceInfoList>();
            LoadDevicesCommand = new Command(async () => await ExecuteLoadDevicesCommand());

            MessagingCenter.Subscribe<DeviceDetailViewModel, DeviceInfo>(this, "DeleteDevice", async (obj, device) =>
            {
                foreach (var g in DevicesListContainer)
                {
                    //foreach (var d in g)
                    //{
                    //   if (d == device)
                    //  {
                   
                    int idx = g.IndexOf(device);
                    if (idx > -1)
                    {
                        g.RemoveAt(idx);
                        await Application.Current.MainPage.Navigation.PopModalAsync(true);
                        break;
                    }

                      //  }
                    //}
                }

                //var newItem = item as Item;
                //Items.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
            });
        }

        private async Task ExecuteLoadDevicesCommand()
        {
            IsBusy = true;

            try
            {
                
                var devices = await DevicesDataStore.GetItemsAsync(true);
                Devices.Clear();


                foreach (var device in devices)
                {
                    Devices.Add(device);
                }

               
                DeviceInfoList d = new DeviceInfoList();
                d.Add(new DeviceInfo() { Description = "Κουζίνα" });
                d.Add(new DeviceInfo() { Description = "Σαλόνι" });

                d.Heading = "Σπίτι";

                DevicesListContainer.Clear();
                DevicesListContainer.Add(d);

                DeviceInfoList d2 = new DeviceInfoList();
                d2.Add(new DeviceInfo() { Description = "Αποθήκη" });
                d2.Add(new DeviceInfo() { ID=130, Description = "Ταμείο" });
                d2.Add(new DeviceInfo() { ID=120, Description = "Είσοδος" });
                d2.Heading = "Μαγαζί";
                DevicesListContainer.Add(d2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
