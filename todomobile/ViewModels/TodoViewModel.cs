using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using todomobile.Models;
using todowpf.Services;

namespace todomobile.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Todo> todos = new ObservableCollection<Todo>();

        private HttpClient _client;

        public ISelectedTodoService SelectedTodo { get; }

        public TodoViewModel(IHttpClientFactory factory, ISelectedTodoService selectedTodo)
        {
            _client = factory.CreateClient("Api");
            GetTodos();
            SelectedTodo = selectedTodo;
        }
        [RelayCommand]
        public async Task GetTodos()
        {
            var handler = new JwtSecurityTokenHandler();
            var token = await SecureStorage.GetAsync("Jwt");
            var jwtToken = handler.ReadJwtToken(token);
            jwtToken.Payload.TryGetValue("userid", out var userid);
            var todos = await _client.GetFromJsonAsync<List<Todo>>($"/todo/{userid}");
            if (todos.Any())
            {
                this.todos.Clear();
                foreach (var todo in todos)
                {
                    this.todos.Add(todo);
                }
            }
        }
        [RelayCommand]
        public async Task OpenTodo(Todo todo)
        {
            //var json = JsonSerializer.Serialize(todo);
            //await SecureStorage.SetAsync("todo", json);
            SelectedTodo.Todo = todo;
            await Shell.Current.GoToAsync("//EditTodo");
        }
        [RelayCommand]
        public async Task AddTodo()
        {
            await Shell.Current.GoToAsync("//AddTodo");
        }
        [RelayCommand]
        public async Task Logout()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
