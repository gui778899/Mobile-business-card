using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using pwapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PWApp.ViewModels
{
    public partial class SignPageViewModel : ObservableObject
    {
        private readonly FirebaseAuthClient _client;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private string _copyOfPassword;

        [ObservableProperty]
        private bool _RememberUser = false;

        [ObservableProperty]
        private int _selectedTabIndex = 0;

        [ObservableProperty]
        private string _errorLogText = "";

        [ObservableProperty]
        private string _errorRegText = "";

        private ICommand _signInCommand;
        public ICommand SignInCommand { get => _signInCommand; }
        private ICommand _signUpCommand; 
        public ICommand SignUpCommand { get => _signUpCommand; }
        private ICommand _clearInputsCommand;
        public ICommand ClearInputsCommand { get => _clearInputsCommand; }

        public SignPageViewModel(FirebaseAuthClient client)
        {
            _client = client;
            _signInCommand = new Command(OnSignIn); 
            _signUpCommand = new Command(OnSignUp);
            _clearInputsCommand = new Command(ClearInputs);
        }
        

        private async void OnSignIn()
        {
            try
            {
                var credential = await _client.SignInWithEmailAndPasswordAsync(Email, Password);
                if (credential != null)
                {
                    string userId = credential.User.Uid;
                    Preferences.Set("user_key", userId);
                    Preferences.Set("remembered", RememberUser);
                    App.Current.MainPage = new AppShell();
                }
            }
            catch (Exception ex)
            {
                Email = string.Empty;
                Password = string.Empty;
                ErrorLogText = "Credentials are wrong!";
            }
        }

        private async void OnSignUp()
        {
            try
            {
                string session = string.Empty;
                if (Password != CopyOfPassword)
                {
                    ErrorRegText = "Passwords do not match!";
                    CopyOfPassword = string.Empty;
                    Password = string.Empty;
                    return;
                }
                var credential = await _client.CreateUserWithEmailAndPasswordAsync(Email, Password);
                if (credential != null)
                {
                    string userId = credential.User.Uid;
                    Preferences.Set("user_key", userId);
                    Preferences.Set("remembered", RememberUser);
                    App.Current.MainPage = new AppShell();
                }
            }
            catch(Exception ex)
            {
                ErrorRegText = "This email is already used!";
            }
        }
        private void ClearInputs()
        {
            Email = string.Empty;
            Password = string.Empty;
            CopyOfPassword = string.Empty;
            ErrorLogText = string.Empty;
            ErrorRegText = string.Empty;
            RememberUser = false;
        }
    }
}
