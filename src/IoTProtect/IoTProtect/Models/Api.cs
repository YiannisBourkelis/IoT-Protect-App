using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace IoTProtect.Models
{
    public static class Api
    {
        public const string Url = "https://iot.filoxeni.com";
        //public const string Url = "https://192.168.40.174:44300";
        //public const string Url = "https://192.168.11.112:44300";

        public static string Token { get; set; } = "DCRkyCeUXE1u44z6rBLnWJ6STjRSzpgiRbTYYOa9";
        //public static string Token { get; set; } = "ZM60zgwx23bMyehOZnolnCr2cqm4ab7RjtftecrL";

        public static HttpClient AuthHttpClient { get; set; }
        public static HttpClient HttpClient { get; set; }

        public static void InitAuthHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

            //δημιουργώ το instance το οποίο το χρησιμοποιώ σε όλη τη διάρκεια της εφαρμογής
            AuthHttpClient = new HttpClient(handler);


            //δοκίμασα να θέσω τα headers για keep alive αλλα για κάποιο λόγο στο server
            //που το έλεγξα το connection το κρατάει περίπου για 60secs παρόλο που έχω ορίσει
            //να το κρατάει για 600secs και στο server και εδώ
            //TODO: να δω αν μπορώ να αυξήσω τη διάρκεια του connection ωστε να μειώσω το latency
            Api.AuthHttpClient.DefaultRequestHeaders.ConnectionClose = false;
            Api.AuthHttpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
            Api.AuthHttpClient.DefaultRequestHeaders.Add("keep-alive", "timeout=600");
            //Api.AuthHttpClient.DefaultRequestHeaders.Host = "iotprotect.local";

            //laravel api authentication related headers
            Api.AuthHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Api.Token);
            Api.AuthHttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }


}

