using System;
using IoTProtect.Models;
using System.ComponentModel;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Security;

namespace IoTProtect.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        //private static readonly HttpClient httpClient = new HttpClient();
        private static UserInfo _userInfo { get; set; }

        public Command SignupCommand { get; }

        public event EventHandler<EventArgs> OnSigneupCompleted;

        public SignupViewModel()
        {
            if (_userInfo == null)
            {
                _userInfo = new UserInfo();
            }

            SignupCommand = new Command(async () => await DoSignupCommandAsync());


        }

        private async Task DoSignupCommandAsync()
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
                        //TODO: only for local use with self signed certificates
                        //TODO: Should add conditional compilation flags
                        return true;
                    };
                    using (var httpClient = new HttpClient(httpClientHandler))
                    {
                        //var content = new StringContent($"{{\"name\":\"yiannis\"}}", System.Text.Encoding.UTF8, "application/json");

                        //var content = new StringContent($"{{\"name\" : \"alfredo\",\"email\" : \"{Email}\",\"password\" : \"8888\",\"password_confirmation\" : \"8888\"}}", System.Text.Encoding.UTF8, "application/json");
                        //var content = new StringContent($"{{\"name\" : \"{FirstName}\",\"email\" : \"{Email}\",\"password\" : \"{Password}\",\"password_confirmation\" : \"{PasswordConfirmation}\"}}", System.Text.Encoding.UTF8, "application/json");
                        var content = new StringContent(
                            $@"{{
                            ""name"" : ""{FirstName}"",
                            ""email"" : ""{Email}"",
                            ""password"" : ""{Password}"",
                            ""password_confirmation"" : ""{PasswordConfirmation}""
                        }}",
                            System.Text.Encoding.UTF8, "application/json");


                        httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

                        var response = await httpClient.PostAsync("https://iotprotect.local/api/auth/signup", content);
                        var responseString = await response.Content.ReadAsStringAsync();


                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            Console.WriteLine("created");


                        }
                        
                    }
                }

            }
            catch (Exception e)
            {

            }
            finally
            {

                IsBusy = false;
                OnSigneupCompleted?.Invoke(this, null);
            }

        }

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

        public string PasswordConfirmation
        {
            set
            {
                _userInfo.PasswordConfirmation = value;
                OnPropertyChanged("PasswordConfirmation");
            }
            get
            {
                return _userInfo.PasswordConfirmation;
            }
        }

        public string FirstName {
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
    }
}
