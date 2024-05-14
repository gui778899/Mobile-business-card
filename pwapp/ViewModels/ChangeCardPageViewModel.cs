using CommunityToolkit.Mvvm.ComponentModel;
using PWApp.Data.Models;
using PWApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PWApp.ViewModels
{
    [QueryProperty("CardData","CardData")]
    public partial class ChangeCardPageViewModel : ObservableObject
    {
        private readonly DataService _service;
        [ObservableProperty]
        private CustomerModel _cardData;

        ICommand _saveCardCommand;
        ICommand _deleteCardCommand;

        public ICommand SaveCardCommand { get { return _saveCardCommand; } }   
        public ICommand DeleteCardCommand {  get { return _deleteCardCommand; } }

        public ChangeCardPageViewModel(DataService service)
        {
            _saveCardCommand = new Command(SaveCard);
            _deleteCardCommand = new Command(DeleteCard);
            _service = service;
        }

        private async void SaveCard()
        {
            _service.ChangeCard(CardData);
            await Shell.Current.GoToAsync($"//MainPage", true);
        }

        private async void DeleteCard()
        {
            _service.RemoveCard(CardData);
            await Shell.Current.GoToAsync($"//MainPage", true);
        }
    }
}
