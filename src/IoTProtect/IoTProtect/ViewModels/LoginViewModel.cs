using System;
using IoTProtect.Models;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IoTProtect.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        //private vars
        private static UserInfo _userInfo { get; set; }

        //public commands
        public Command LoginCommand { get; }

        //public events
        public event EventHandler<EventArgs> OnLoginCompleted;
        public event EventHandler<ExceptionEventArgs> OnUnhandledException;

        //public properties
        public string Email
        {
            set
            {
                _userInfo.Email = value;
                OnPropertyChanged("Email");
            }
            get
            {
                return _userInfo.Email;
            }
        }

        public string Password
        {
            set
            {
                _userInfo.Password = value;
                OnPropertyChanged("Password");
            }
            get
            {
                return _userInfo.Password;
            }
        }

        public string FirstName
        {
            set
            {
                _userInfo.FirstName = value;
                OnPropertyChanged("FirstName");
            }
            get
            {
                return _userInfo.FirstName;
            }
        }

        //constructor
        public LoginViewModel()
        {
            if (_userInfo == null)
            {
                _userInfo = new UserInfo();
            }

            LoginCommand = new Command(async () => await DoLoginCommandAsync());
        }

        private async Task DoLoginCommandAsync()
        {
            try
            {
                IsBusy = true;

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
                        var content = new StringContent(
                            $@"{{
                            ""email"" : ""{Email}"",
                            ""password"" : ""{Password}"",
                            ""rememder_me"" : true
                        }}",
                            System.Text.Encoding.UTF8, "application/json");


                        httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                        var response = await httpClient.PostAsync("http://iotprotect.local/api/auth/login", content);
                        var responseString = await response.Content.ReadAsStringAsync();


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Console.WriteLine("logged in!");

                            var json = JObject.Parse(responseString);  //JsonDocument.Parse(responseString);
                            var access_token = json.Value<string>("access_token"); //json.RootElement.GetProperty("access_token").GetString();
                            _userInfo.AccessToken = access_token;

                            Console.WriteLine("ok");
                            MessagingCenter.Send(this, "LoginCompleted", "ok");
                            //OnLoginCompleted?.Invoke(this, null);
                            
                        }

                    }
                }

            }
            catch (Exception e)
            {
                OnUnhandledException?.Invoke(this, new ExceptionEventArgs(e));

            }
            finally
            {

                IsBusy = false;
                OnLoginCompleted?.Invoke(this, null);
            }

        }
    }
}
