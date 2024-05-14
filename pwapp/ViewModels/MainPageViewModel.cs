using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Firebase.Auth;
using PWApp.Theme;
using PWApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pwapp;

namespace PWApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private FirebaseAuthClient _client;
        private ICommand _generateQrCode;
        private ICommand _viewCards;
        private ICommand _scanQrCode;
        private ICommand _logoutCommand;
        private ICommand _changeThemeCommand;

        public ICommand ChangeThemeCommand {get =>  _changeThemeCommand;}
        public ICommand LogoutCommand { get => _logoutCommand; }
        public ICommand GenerateQrCodeCommand { get { return _generateQrCode; } }
        public ICommand ViewCardsCommand { get { return _viewCards; } }
        public ICommand ScanQrCodeCommand { get { return _scanQrCode; } }

        public MainPageViewModel(FirebaseAuthClient client)
        {
            _client = client;
            _generateQrCode = new Command(GenerateQrCode);
            _viewCards = new Command(ViewCards);
            _scanQrCode = new Command(ScanQrCode); 
            _logoutCommand = new Command(Logout);
            _changeThemeCommand = new Command(ChangeTheme);
        }

        private async void GenerateQrCode()
        {
            await Shell.Current.GoToAsync($"{nameof(NewCardPage)}", true);
        }

        private async void ViewCards()
        {
            await Shell.Current.GoToAsync($"{nameof(CardsPage)}", true);
        }

        private async void ScanQrCode()
        {


            await Shell.Current.GoToAsync($"{nameof(QrCodePage)}", true);
        }

        private void Logout()
        {
            Preferences.Clear();
            App.Current.MainPage = new SignPage(_client);
        }
        private void ChangeTheme()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(App.IsDark ? new LightTheme() : new DarkTheme());
                App.IsDark = !App.IsDark;
            }
        }
    }
}
