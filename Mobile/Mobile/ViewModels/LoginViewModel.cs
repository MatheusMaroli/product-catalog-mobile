using Mobile.Repository;
using Mobile.Views;
using System;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        public string Email {
            get => _email;
            set => SetProperty(ref _email, value); 
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public Command LoginCommand { get; }

        private LoginRepository _repository;

        public LoginViewModel()
        {
            _repository = new LoginRepository(_catalogApi);
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    ShowFailNotification("Preencha email e senha");
                    return;
                }


                if (await _repository.Login(Email, Password))
                {
                    await Shell.Current.GoToAsync($"//{nameof(CatalogPage)}");
                }
                else
                    ShowFailNotification("Usuário ou senha invalido");
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e.ToString());
                ShowFailNotification("Falha para realizar o login");
            }
            finally
            {
                IsBusy = false;
            }


        }
    }
}
