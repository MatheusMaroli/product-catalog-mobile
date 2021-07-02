using Mobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Repository
{
    public class LoginRepository : BaseRepository
    {
        public LoginRepository(CatalogApiService api) : base(api)
        {
        }

        public async Task<bool> Login(string email, string password)
        {
            var apiResponse = await _api.Post("login", new { Email = email, Password = password });

            if (apiResponse.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
