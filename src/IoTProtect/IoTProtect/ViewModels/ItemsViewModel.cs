using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using System.Net.Http;
using System.Net.Security;
//using System.Text.Json;
using Newtonsoft.Json;

using IoTProtect.Models;
using IoTProtect.Views;

namespace IoTProtect.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        //private vars
        private Timer itemsReloadTimer;

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            SetupTimer();

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
            });

            MessagingCenter.Subscribe<LoginViewModel, string>(this, "LoginCompleted", (sender, arg) =>
            {
                Console.WriteLine("ggggg");
                itemsReloadTimer.Enabled = true;

                //await DisplayAlert("Message received", "arg=" + arg, "OK");
            });
        }

        private void SetupTimer()
        {
            // Create a timer with a two second interval.
            itemsReloadTimer = new System.Timers.Timer(3000);
            // Hook up the Elapsed event for the timer. 
            itemsReloadTimer.Elapsed += ItemsReloadTimer_Elapsed;
            itemsReloadTimer.AutoReset = false;
            itemsReloadTimer.Enabled = true;
            
        }

        private async Task DoReloadDevicesInfoAsync()
        {
            try
            {
                //IsBusy = true;

                using (var httpClientHandler = new HttpClientHandler())
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
                    {
                        if (sslPolicyErrors == SslPolicyErrors.None)
                        {
                            return true;   //Is valid
                        }
#if DEBUG
                        Console.WriteLine("HttpClientHandler > Accept self signed certificate");
                        return true;
#else
                        return false;
#endif
                    };
                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIxIiwianRpIjoiMTA0MTlkYWQyNjlkZjUxOTA4MGU0YmEzYjA1ZDhkYjgxOWI5YzQ3ZTA1MDQyZDlkYmZkZDQ2NmI5OWIwZGU5NzA4N2YwYTA2ODFmNGJlMzgiLCJpYXQiOjE1ODg1Mzg2MjcsIm5iZiI6MTU4ODUzODYyNywiZXhwIjoxNjIwMDc0NjI3LCJzdWIiOiIxIiwic2NvcGVzIjpbXX0.dZHdd7bLViB3WoQJGvtWlu0cqXd4bE38-I4iMRQsCR8SbcZFW1qx1WDdEL_l7-ZaTdOrxP_r8_oaHJwk_ZKx-PAPM5poXIUP6b1s2UebXTtkUZhrnmDctGdfi4kDLbPUfmKs-xHEsHCOVxS0H8IsWvhA18Gi81FfOr43vhIOFgzdS6zSQyBeRyZ1cQu5RgDsmbCd2Ql6doB-EgqdGMxK61OOUVFJQSnCWMhLp3NAfFKizmR2mZcxMYJfcu1IH7t13Q-B7o78etTFz2bKKUVG4j8WwEEGuBcYyIHGGKto2KqojJANZV4Xu4npsLkUaD1J_Vbfdu9fdMSqVvvBKTlSfai-dUytetjeAAJYrlhyLt9B2Z4qpFMq1SQc67TtwPnQjaFK6gNZ88PxmaBAFSIG5ivtKYEDc7qIzNqXtU53XplDvzjXLpi4seSdiviGmgbn0gNg-gl9ZpBDPgUwr8hKxjtkYev5l5Cid5WCxtvRyU4Rt5FNijM8Ijk4LucLYtgPMPnjII1KNOK913LXjsT124aof84LE2SPH5EqChif2UPjSt4TM5ZzY5V6jh6So53cJWfjEnMXw7162OI5nN-04fTLwaT9dw5DlWLy9foD9sbVpMSgWGmXozU4_nVgqczr3_GRhFBMIpQztfPGEVbyVX0I0jeb8hA-v-pk-6c_PFE");

                        var response =  await httpClient.GetAsync("https://iotprotect.local/api/auth/test");
                        var responseString =  await response.Content.ReadAsStringAsync();


                        //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            

                            var t = JsonConvert.DeserializeObject<IoTProtectDeviceInfo>(responseString);
                            /*
                            var options = new JsonSerializerOptions();
                            

                            var t = JsonSerializer.Deserialize<IoTProtectDeviceInfo>(responseString, new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            });
                            */
                            Console.WriteLine(t.Photoresitor);
                        }

                    }
                }

            }
            catch (Exception e)
            {
                //OnUnhandledException?.Invoke(this, new ExceptionEventArgs(e));

            }
            finally
            {

                //IsBusy = false;
                // OnSigneupCompleted?.Invoke(this, null);
                itemsReloadTimer.Start();
            }

        }


        private void ItemsReloadTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            DoReloadDevicesInfoAsync();
        }

        async Task ExecuteLoadItemsCommand()
        {
            /*
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            */
        }

    }
}