using Mobile.Models;
using Mobile.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;

namespace Mobile.Repository
{
    public abstract class BaseRepository 
    {

        protected readonly CatalogApiService _api;

        public BaseRepository(CatalogApiService api)
        {
            _api = api;
        }

      
    }
}
