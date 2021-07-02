using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class CatalogApiService
    {
        private string _token = "";
        private readonly string _baseUrl = "";
        public CatalogApiService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        private AuthenticationHeaderValue SetTokenHeader()
        {
            if (Connect())
                return new AuthenticationHeaderValue("Bearer", _token);
            return null;
        }

        public bool Connect()
        {
            return !string.IsNullOrEmpty(_token);
        }


        public void SetToken(object token)
        {
            _token = (string)token;
        }

        public Task<HttpResponseMessage> Get(string route)
        {

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = SetTokenHeader();
            return _client.GetAsync($"{_baseUrl}/{route}");

        }

        public Task<HttpResponseMessage> Post(string route, object param)
        {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = SetTokenHeader();

            var _jsonParameters = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
            return _client.PostAsync($"{_baseUrl}/{route}", _jsonParameters);

        }

        public Task<HttpResponseMessage> Put(string route, object param)
        {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = SetTokenHeader();

            var _jsonParameters = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
            return _client.PutAsync($"{_baseUrl}/{route}", _jsonParameters);
        }

        public Task<HttpResponseMessage> Delete(string route)
        {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = SetTokenHeader();

            return _client.DeleteAsync($"{_baseUrl}/{route}");
        }
    }
}
