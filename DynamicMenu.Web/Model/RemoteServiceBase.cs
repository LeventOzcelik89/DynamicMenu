using Azure;
using DynamicMenu.API.DTOs;
using DynamicMenu.Core.Models;
using Newtonsoft.Json;
using System.Text;

namespace DynamicMenu.Web.Model
{
    public abstract class RemoteServiceBase
    {
        public abstract string baseAddress { get; }

        private readonly HttpClient _httpClient;

        protected RemoteServiceBase(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> PostJsonData<T>(string url, object data) where T : class
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(new Uri(baseAddress + url), data);
                var result = await response.Content.ReadAsStringAsync();
                var resultJson = JsonConvert.DeserializeObject<T>(result);

                return resultJson;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResultStatus<T>?> PostJsonDataResultStatus<T>(string url, object data) where T : class
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(new Uri(baseAddress + url), data);
                var result = await response.Content.ReadAsStringAsync();
                var resultJson = JsonConvert.DeserializeObject<ResultStatus<T>>(result);

                return resultJson;
            }
            catch (Exception ex)
            {
                return ResultStatus<T>.Error(ex.Message);
            }
        }

        public async Task<T?> DeleteData<T>(string url) where T : class
        {
            try
            {
                var response = await _httpClient.DeleteAsync(new Uri(baseAddress + url));
                var result = await response.Content.ReadAsStringAsync();
                var resultJson = JsonConvert.DeserializeObject<T>(result);

                return resultJson;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<T?> GetData<T>(string url) where T : class
        {
            try
            {
                var str = await _httpClient.GetStringAsync(new Uri(baseAddress + url));
                var result = JsonConvert.DeserializeObject<T>(str);

                return result;
            }
            catch (Exception ex)
            {
                return null;
                //  return ex.Message;
            }
        }

        public async Task<ResultStatus<T>> GetDataResultStatus<T>(string url) where T : class
        {
            try
            {
                var str = await _httpClient.GetStringAsync(new Uri(baseAddress + url));
                var result = JsonConvert.DeserializeObject<ResultStatus<T>>(str);

                return result ?? ResultStatus<T>.Error("Null response from API server.");
            }
            catch (Exception ex)
            {
                return ResultStatus<T>.Error(ex.Message);
            }
        }

        public async Task<string?> GetData(string url)
        {
            try
            {
                var str = await _httpClient.GetStringAsync(new Uri(baseAddress + url));
                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
