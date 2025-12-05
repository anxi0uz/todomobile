using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using todomobile.Models;

namespace todomobile.Services
{
    public class AuthService : IAuthService
    {
        private HttpClient _client;

        public AuthService(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Api");
        }
        public async Task<AuthResponse?> Login(AuthRequest request)
        {
            var response = await _client.PostAsJsonAsync("/user/login", request);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();
                return authResponse;
            }
            return null;
        }
    }
}
