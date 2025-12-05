using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using todomobile.Models;

namespace todomobile.ViewModels
{
    public partial class AddTodoViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;


        private HttpClient _client;

        public AddTodoViewModel(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("Api");
        }

        [RelayCommand]
        public async Task AddTodo()
        {
            var handler = new JwtSecurityTokenHandler();
            var token = await SecureStorage.GetAsync("Jwt");
            var jwtToken = handler.ReadJwtToken(token);
            jwtToken.Payload.TryGetValue("userid", out var userid);
            var id = Convert.ToInt32(userid);
            var todoRequest = new TodoRequest(id, _name,_description);
            var response = await _client.PostAsJsonAsync("/todo",todoRequest);
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Успешно", "Успешно обновлено!", "Ok");
            }
            await Shell.Current.GoToAsync("//TodoPage");
        }

    }
}
