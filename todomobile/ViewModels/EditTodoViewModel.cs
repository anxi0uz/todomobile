using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using todomobile.Models;
using todowpf.Services;

namespace todomobile.ViewModels
{
    public partial class EditTodoViewModel : ObservableObject
    {
        private HttpClient _client;
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;
        [ObservableProperty]
        private int _userId;
        public Todo Todo { get; set; }
        public EditTodoViewModel(IHttpClientFactory factory,ISelectedTodoService selectedTodo)
        {
            //GetTodo();
            Todo = selectedTodo.Todo;
            _client = factory.CreateClient("Api");
            _name = Todo.Name;
            _description = Todo.Description;
            _userId = Todo.UserId;
        }

        private async Task GetTodo()
        {
            var json = await SecureStorage.GetAsync("todo");
            Todo = JsonSerializer.Deserialize<Todo>(json);
        }
        [RelayCommand]
        public async Task UpdateTodo()
        {
            var model = new TodoRequest(_userId, _name, _description);
            var response = await _client.PutAsJsonAsync($"/todo/{Todo.Id}",model);
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Успешно", "Успешно обновлено!", "Ok");
            }
            await Shell.Current.GoToAsync("//TodoPage");
        }
        [RelayCommand]
        public async Task DeleteTodo()
        {
            var response = await _client.DeleteAsync($"/todo/{Todo.Id}");
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Успешно", "Успешно удалено!", "Ok");
            }
            await Shell.Current.GoToAsync("//TodoPage");
        }
    }
}
