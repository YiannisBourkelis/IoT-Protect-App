using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IoTProtect.Models;
using Newtonsoft.Json;

namespace IoTProtect.Services
{
    public class BaseRestService<T> where T : IItem<T>, IRest, new()
    {
        public BaseRestService()
        {
        }

        public T ItemChild { get; set; } = new T();

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

        public async Task<SimplePaginator<T>> ReadItemsAsync()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            Uri uri = null;
            if (ItemChild.ReadItemsNextPageUrl != null)
            {
                Uri next_page_uri = new Uri(ItemChild.ReadItemsNextPageUrl);
                uri = new Uri($"{Api.Url}{next_page_uri.PathAndQuery}");
                Console.WriteLine($"ReadItemsAsync - Will read next_page_uri: {uri}");
            }
            else
            {
                uri = new Uri($"{Api.Url}{ItemChild.ReadItemsRoutePath}");
            }

            HttpResponseMessage response = await Api.AuthHttpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

            var paginator = new SimplePaginator<T>();
            if (response.IsSuccessStatusCode)
            {
                var items_list_str = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(items_list_str))
                {
                    paginator = JsonConvert.DeserializeObject<SimplePaginator<T>>(items_list_str);
                    ItemChild.ReadItemsNextPageUrl = paginator.NextPageUrl;
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

