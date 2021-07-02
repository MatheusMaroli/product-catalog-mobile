using Mobile.Models;
using Mobile.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Repository
{
    public class TagRepository : BaseRepository
    {
        public TagRepository(CatalogApiService api) : base(api)
        {
        }


        public async Task<IEnumerable<Tag>> GetAll()
        {
            var apiResponse = await _api.Get("tag");

            if (apiResponse.IsSuccessStatusCode)
            {
                var json = apiResponse.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<Tag>>(json);
            }
            throw new System.Exception();
        }


        public async Task<IEnumerable<ProductCatalog>> GetProductCatalog(int tagId)
        {
            var apiResponse = await _api.Get($"tag/{tagId}/catalog");

            if (apiResponse.IsSuccessStatusCode)
            {
                var json = apiResponse.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<ProductCatalog>>(json);
            }
            throw new System.Exception();
        }
    }
}
