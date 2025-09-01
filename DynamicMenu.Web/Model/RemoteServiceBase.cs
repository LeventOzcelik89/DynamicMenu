using Azure;
using DynamicMenu.API.DTOs;
using Newtonsoft.Json;
using System.Text;

namespace DynamicMenu.Web.Model
{
    public abstract class RemoteServiceBase
    {
        public abstract string baseAddress { get; }

        private readonly HttpClient _httpClient;
        //public readonly ConfigManager _configManager;

        //protected RemoteServiceBase(HttpClient httpClient, ConfigManager configManager)
        //{
        //    _httpClient = httpClient;
        //    _configManager = configManager;
        //}

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
                //Log.Error(this.GetType().Name + " DownloadString Ex: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
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
                //Log.Error(this.GetType().Name + " DownloadString Ex: " + ex.Message + Environment.NewLine + ex.StackTrace);
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
                //Log.Error(this.GetType().Name + " DownloadString Ex: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
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
                //Log.Error(this.GetType().Name + " DownloadString Ex: " + ex.Message + Environment.NewLine + ex.StackTrace);
                return null;
            }
        }

    }
}
