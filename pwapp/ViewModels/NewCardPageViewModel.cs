using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using pwapp;
using PWApp.Data.Models;
using PWApp.Data.Services;
using PWApp.Helpers;
using PWApp.Views.PopPages;
using System.Windows.Input;

namespace PWApp.ViewModels
{
    public partial class NewCardPageViewModel : ObservableObject
    {
        private readonly DataService _service;

        [ObservableProperty]
        private CustomerModel _cardData = new CustomerModel();

        private ICommand _generateCardCommand;
        public ICommand GenerateCardCommand { get => _generateCardCommand; } 

        public NewCardPageViewModel(DataService service)
        {
            _generateCardCommand = new Command(GenerateCard);
            _service = service;
        }

        private async void GenerateCard()
        {
            byte[] qrData = null;
            try
            {
                await Task.Run(async () =>
                {
                    CardData.IsMyData = true;
                    CardData.FirstName.Trim();
                    CardData.SecondName.Trim();
                    CardData.CreateBy = Preferences.Get("user_key", "");
                    CardData.Email.Trim();
                    CardData.FirstPhoneNumber.Trim();
                    CardData.SecondPhoneNumber.Trim();
                    CardData.JobTitle.Trim();
                    CardData.CompanyName.Trim();
                    _service.AddCard(CardData);
                    CardData.IsMyData = false;
                    qrData = QrGenerator.Generate(CardData);
                    CardData = new();
                });
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    Shell.Current.ShowPopup(new NewCardPopup(qrData));
                });
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
            }
        }
    }
}
