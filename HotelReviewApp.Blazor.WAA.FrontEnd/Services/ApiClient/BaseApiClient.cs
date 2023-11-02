using System.Net.Http.Json;

namespace HotelReviewApp.Blazor.WAA.FrontEnd.Services.ApiClient
{
    public class BaseApiClient
    {
        protected readonly HttpClient _httpClient;

        public BaseApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        protected async Task<T> GetAsync<T>(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode(); 

            var data = await response.Content.ReadFromJsonAsync<T>();
            return data;
        }

        
        protected async Task<T> PostAsync<T>(string uri, T item)
        {
            var response = await _httpClient.PostAsJsonAsync(uri, item);
            response.EnsureSuccessStatusCode(); 

            var data = await response.Content.ReadFromJsonAsync<T>();
            return data;
        }

        
        protected async Task PutAsync<T>(string uri, T item)
        {
            var response = await _httpClient.PutAsJsonAsync(uri, item);
            response.EnsureSuccessStatusCode(); 
        }

        
        protected async Task DeleteAsync(string uri)
        {
            var response = await _httpClient.DeleteAsync(uri);
            response.EnsureSuccessStatusCode(); 
        }        
    }

}
