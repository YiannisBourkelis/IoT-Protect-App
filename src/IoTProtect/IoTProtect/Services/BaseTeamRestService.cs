using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IoTProtect.Models;
using Newtonsoft.Json;

namespace IoTProtect.Services
{
    public class BaseTeamRestService<T> where T : IItem<T>, IRest
    {
        public BaseTeamRestService()
        {
        }

        public async Task<bool> CreateItemAsync(T item)
        {
            Uri uri = new Uri($"{Api.Url}{item.CreateItemRoutePath}");

            var item_json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(item_json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Api.AuthHttpClient.PostAsync(uri, content);
            Console.WriteLine($"Rest::CreateItemAsync::{item.GetType()}, statusCode:{response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<SimplePaginator<T>> ReadItemsAsync(T item)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Uri uri = new Uri($"{Api.Url}{item.ReadItemsRoutePath}");

            HttpResponseMessage response = await Api.AuthHttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

            var paginator = new SimplePaginator<T>();
            if (response.IsSuccessStatusCode)
            {
                var items_list_str = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(items_list_str))
                {
                    paginator = JsonConvert.DeserializeObject<SimplePaginator<T>>(items_list_str);
                }
            }
            sw.Stop();
            Console.WriteLine($"ReadItemsAsync: {sw.ElapsedMilliseconds}");
            return paginator;
        }

        public async Task<bool> UpdateItem(T item)
        {
            Uri uri = new Uri($"{Api.Url}{item.UpdateItemRoutePath}");

            var item_json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(item_json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await Api.AuthHttpClient.PutAsync(uri, content);
            Console.WriteLine($"Rest::UpdateItem::{item.GetType()}, statusCode:{response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }


        public async Task<bool> DeleteItemAsync(T item)
        {
            Uri uri = new Uri($"{Api.Url}{item.DeleteItemRoutePath}");

            HttpResponseMessage response = await Api.AuthHttpClient.DeleteAsync(uri);
            Console.WriteLine($"Rest::DeleteItemAsync::{item.GetType()}, statusCode:{response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        } //DeleteDocumentAsync
    }
}

