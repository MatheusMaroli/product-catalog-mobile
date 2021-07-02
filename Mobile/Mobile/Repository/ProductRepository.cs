using Mobile.Models;
using Mobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Repository
{
    public class ProductRepository : BaseRepository
    {
        public ProductRepository(CatalogApiService api) : base(api)
        {
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           
            var apiResponse = await _api.Get("product");
            if (apiResponse.IsSuccessStatusCode)
            {
                var json = apiResponse.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
            }
            throw new System.Exception();          
        }

        public async Task<Product> GetById(int productId)
        {
            
            var apiResponse = await _api.Get($"product/{productId}/detail");

            if (apiResponse.IsSuccessStatusCode)
            {
                var json = apiResponse.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Product>(json);
            }
            throw new System.Exception();   
        }

        public async Task<int> Create(Product product, IEnumerable<Tag> tags)
        {
            var mapParam = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Img = product.Img,
                Tags = tags.Select(currentTag => new { tagId = currentTag.Id })
            };

            var apiResponse = await _api.Post("product", mapParam);

            if (apiResponse.IsSuccessStatusCode)
            {
                var json = apiResponse.Content.ReadAsStringAsync().Result;
                return  JsonConvert.DeserializeObject<int>(json);
            }
            throw new System.Exception();
        }

        public async Task Create1000(Product product, IEnumerable<Tag> tags)
        {
            var mapParam = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Img = product.Img,
                Tags = tags.Select(currentTag => new { tagId = currentTag.Id })
            };

            var apiResponse = await _api.Post("product/create1000", mapParam);

            if (! apiResponse.IsSuccessStatusCode)
                throw new System.Exception();
        }


        public async Task DeleteTag(int productId, int tagId)
        {         
            var apiResponse = await _api.Delete($"product/{productId}/tag/{tagId}");

            if (! apiResponse.IsSuccessStatusCode)
                throw new System.Exception("Fail execute in delete product tag");
        }

        public async Task AddProductTag(int productId, int tagId)
        {
            
            var apiResponse = await _api.Post($"product/{productId}/tag", new { TagId = tagId  });

            if (!apiResponse.IsSuccessStatusCode)
                throw new System.Exception("Fail execute in delete product tag");
        }

        public async Task Update(Product product)
        {
            var mapParam = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Img = product.Img
            };

            var apiResponse = await _api.Put($"product/{product.Id}", mapParam);

            if (! apiResponse.IsSuccessStatusCode)
                throw new System.Exception();
        }
    }
}
