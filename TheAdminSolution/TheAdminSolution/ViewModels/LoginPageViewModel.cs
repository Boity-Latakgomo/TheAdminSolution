using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheAdminSolution.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        INavigationService _navigationService;
        public string Username { get; set; }
        public string Password { get; set; }
        public DelegateCommand LoginCommand { get; }
        public LoginPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new DelegateCommand(OnLogin);
        }
        private async void OnLogin()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                UserDialogs.Instance.Toast("Fill all the fields");
            }
            else if (Username == "AuthAdmin" && Password == "123456")
            {
                await _navigationService.NavigateAsync("NavigationPage/MainPage");
            }
            else
            {
                UserDialogs.Instance.Toast("Incorrect username or password");
            }
        }
    }
}
