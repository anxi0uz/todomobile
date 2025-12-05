using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todomobile.Models;
using todomobile.Services;

namespace todomobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty] public string _username = string.Empty;

        [ObservableProperty] public string _password = string.Empty;
        public LoginViewModel(IAuthService authService)
        {
            AuthService = authService;
        }

        public IAuthService AuthService { get; }

        [RelayCommand]
        public async Task Login()
        {
            var model = new AuthRequest(_password, _username);
            var response = await AuthService.Login(model);
            if (response != null)
            {
                await SecureStorage.SetAsync("Jwt", response.AccessToken);
                await Shell.Current.GoToAsync("//TodoPage");
            }
        }
    }
}
