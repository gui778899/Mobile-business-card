using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using PWApp.Bindable;
using PWApp.Data.Models;
using PWApp.Data.Services;
using PWApp.Views.PopPages;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace PWApp.ViewModels
{
    public partial class CardsPageViewModel : ObservableObject
    {
        public static string GeneratedSearchOption { get; set; } = "Name";
        public static string ScannedSearchOption { get; set; } = "Name";
        private readonly DataService _service;

        private ObservableCollection<CustomerModelBindable> _generated = new ObservableCollection<CustomerModelBindable>();
        private ObservableCollection<CustomerModelBindable> _scanned = new ObservableCollection<CustomerModelBindable>();

        [ObservableProperty]
        private ObservableCollection<CustomerModelBindable> _myCards = new();
        [ObservableProperty]
        private ObservableCollection<CustomerModelBindable> _otherCards = new();
        [ObservableProperty]
        private int _selectedTab = 0;
        [ObservableProperty]
        private bool _isMyCardsBusy = false;
        [ObservableProperty]
        private bool _isOtherCardsBusy = false;
        private bool _byName = false;
        public bool ByName
        {
            get => _byName;
            set
            {
                _byName = value;
                OnPropertyChanged();
            }
        }
        private bool _byEmail = false;
        public bool ByEmail
        {
            get => _byEmail;
            set
            {
                _byEmail = value;
                OnPropertyChanged();
            }
        }
        private bool _byPhone = false;
        public bool ByPhone
        {
            get => _byPhone;
            set
            {
                _byPhone = value;
                OnPropertyChanged();
            }

        }
        private bool _byJobTitle = false;
        public bool ByJobTitle
        {
            get => _byJobTitle;
            set
            {
                _byJobTitle = value;
                OnPropertyChanged();
            }
        }
        private bool _byCompany = false;
        public bool ByCompany
        {
            get => _byCompany;
            set
            {
                _byCompany = value;
                OnPropertyChanged();
            }
        }
        private ICommand _resfershMyCardsCommand;
        public ICommand RefreshMyCardsCommand { get => _resfershMyCardsCommand; }
        private ICommand _refreshScannedCardsCommand;
        public ICommand RefreshScannedCardsCommand { get => _refreshScannedCardsCommand; }
        private ICommand _generateNewCardCommand;
        public ICommand GenerateNewCardCommand { get => _generateNewCardCommand; }
        private ICommand _scanNewCardCommand;
        public ICommand ScanNewCardCommand { get => _scanNewCardCommand; }
        private ICommand _generatedCardTappedCommand;
        public ICommand GeneratedCardTappedCommand { get => _generatedCardTappedCommand; }
        private ICommand _optionsScannedTappedCommand;
        public ICommand OptionsScannedTappedCommand { get => _optionsScannedTappedCommand; }
        private ICommand _optionsGeneratedTappedCommnad;
        public ICommand OptionsGeneratedTappedCommand { get => _optionsGeneratedTappedCommnad; }
        private ICommand _searchScannedCardCommand;
        public ICommand SearchScannedCommand { get => _searchScannedCardCommand; }
        private ICommand _searchGeneratedCardCommand;
        public ICommand SearchGeneratedCardCommand { get => _searchGeneratedCardCommand; }
        private ICommand _generatedSortingOptionCommand;
        public ICommand GeneratedSortingOptionCommand { get => _generatedSortingOptionCommand; }

        public CardsPageViewModel(DataService service)
        {
            _service = service;
            _generateNewCardCommand = new Command(GenerateNewCard);
            _scanNewCardCommand = new Command(ScanNewCard);
            _optionsGeneratedTappedCommnad = new Command(OptionsGeneratedTapped);
            _optionsScannedTappedCommand = new Command(OptionsScannedTapped);
            _searchGeneratedCardCommand = new Command<object>(SearchGenerated);
            _searchScannedCardCommand = new Command<object>(SearchScanned);
            _generatedCardTappedCommand = new Command<string>(GeneratedCardTapped);
            _resfershMyCardsCommand = new Command(RefreshMyCards);
            _refreshScannedCardsCommand = new Command(RefreshScannedCards);
            _generatedSortingOptionCommand = new Command<string>(SortingOptionTapped);
            RefreshMyCards();
            RefreshScannedCards();
            GeneratedSearchOption = "Name";
            ScannedSearchOption = "Name";
        }

        private void RefreshMyCards()
        {
            IsMyCardsBusy = true;
            MyCards.Clear();
            string userId = Preferences.Get("user_key", string.Empty);
            var allCards = _service.GetAllMyCards(true, userId);
            foreach (var card in allCards)
            {
                MyCards.Add(new CustomerModelBindable(card));
            }
            _generated = MyCards;
            IsMyCardsBusy = false;
        }

        private void RefreshScannedCards()
        {
            IsOtherCardsBusy = true;
            OtherCards.Clear();
            string userId = Preferences.Get("user_key", string.Empty);
            var allCards = _service.GetAllMyCards(false, userId);
            foreach (var card in allCards)
            {
                OtherCards.Add(new CustomerModelBindable(card));
            }
            _scanned = OtherCards;
            IsOtherCardsBusy = false;
        }

        private async void GeneratedCardTapped(string customerId)
        {
            var neededCard = _service.FindCardById(customerId);
            Dictionary<string, object> data = new Dictionary<string, object>() { { "CardData", neededCard } };
            await Shell.Current.GoToAsync(nameof(ChangeCardPage), true, data);
        }

        private async void OptionsScannedTapped()
        {
            await Shell.Current.ShowPopupAsync(new OptionsPopup(false));
        }

        private async void OptionsGeneratedTapped()
        {
            await Shell.Current.ShowPopupAsync(new OptionsPopup(true));
        }

        private async void SearchScanned(object sender)
        {
            await Task.Run(() =>
            {
                var entry = sender as Entry;
                string text = entry.Text;
                if (string.IsNullOrEmpty(text))
                    OtherCards = _scanned;
                switch (ScannedSearchOption)
                {
                    case "Name":
                        OtherCards = _scanned.Where(x => x.Name.Contains(text)).ToObservableCollection();
                        break;
                    case "Email":
                        OtherCards = _scanned.Where(x => x.Email.Contains(text)).ToObservableCollection();
                        break;
                    case "Phone number":
                        OtherCards = _scanned.Where(x => x.PhoneNumber.Contains(text)).ToObservableCollection();
                        break;
                    case "Job Title":
                        OtherCards = _scanned.Where(x => x.JobTitle.Contains(text)).ToObservableCollection();
                        break;
                    case "Company name":
                        OtherCards = _scanned.Where(x => x.CompanyName.Contains(text)).ToObservableCollection();
                        break;
                    default:
                        return;
                }
            });
        }

        private async void SearchGenerated(object sender)
        {
            await Task.Run(() =>
            {
                var entry = sender as Entry;
                string text = entry.Text;
                if (string.IsNullOrEmpty(text))
                    MyCards = _generated;
                switch (GeneratedSearchOption)
                {
                    case "Name":
                        MyCards = _generated.Where(x => x.Name.Contains(text)).ToObservableCollection();
                        break;
                    case "Email":
                        MyCards = _generated.Where(x => x.Email.Contains(text)).ToObservableCollection();
                        break;
                    case "Phone number":
                        MyCards = _generated.Where(x => x.PhoneNumber.Contains(text)).ToObservableCollection();
                        break;
                    case "Job Title":
                        MyCards = _generated.Where(x => x.JobTitle.Contains(text)).ToObservableCollection();
                        break;
                    case "Company name":
                        MyCards = _generated.Where(x => x.CompanyName.Contains(text)).ToObservableCollection();
                        break;
                    default:
                        return;
                }
            });
        }

        private void SortingOptionTapped(string param)
        {
            UnselectSortingChoices(param);
            switch (param)
            {
                case "Name":
                    ByName = true;
                    if (SelectedTab == 0)
                        MyCards = MyCards.OrderBy(x => x.Name).ToObservableCollection();
                    else
                        OtherCards = OtherCards.OrderBy(x => x.Name).ToObservableCollection();
                    break;
                case "Email":
                    ByEmail = true;
                    if (SelectedTab == 0)
                        MyCards = MyCards.OrderBy(x => x.Email).ToObservableCollection();
                    else
                        OtherCards = OtherCards.OrderBy(x => x.Email).ToObservableCollection();
                    break;
                case "Phone number":
                    ByPhone = true;
                    if (SelectedTab == 0)
                        MyCards = MyCards.OrderBy(x => x.PhoneNumber).ToObservableCollection();
                    else
                        OtherCards = OtherCards.OrderBy(x => x.PhoneNumber).ToObservableCollection();
                    break;
                case "Job Title":
                    ByJobTitle = true;
                    if (SelectedTab == 0)
                        MyCards = MyCards.OrderBy(x => x.JobTitle).ToObservableCollection();
                    else
                        OtherCards = OtherCards.OrderBy(x => x.JobTitle).ToObservableCollection();
                    break;
                case "Company name":
                    ByCompany = true;
                    if (SelectedTab == 0)
                        MyCards = MyCards.OrderBy(x => x.CompanyName).ToObservableCollection();
                    else
                        OtherCards = OtherCards.OrderBy(x => x.CompanyName).ToObservableCollection();
                    break;
                default:
                    return;
            }
        }
        private async void GenerateNewCard() => await Shell.Current.GoToAsync($"{nameof(NewCardPage)}", true);

        private async void ScanNewCard() => await Shell.Current.GoToAsync($"{nameof(QrCodePage)}", true);

        private void UnselectSortingChoices(string param)
        {
            switch (param)
            {
                case "Name":
                    ByEmail = false;
                    ByPhone = false;
                    ByJobTitle = false;
                    ByCompany = false;
                    break;
                case "Email":
                    ByName = false;
                    ByPhone = false;
                    ByJobTitle = false;
                    ByCompany = false;
                    break;
                case "Phone number":
                    ByEmail = false;
                    ByName = false;
                    ByJobTitle = false;
                    ByCompany = false;
                    break;
                case "Job Title":
                    ByEmail = false;
                    ByName = false;
                    ByPhone = false;
                    ByCompany = false;
                    break;
                case "Company name":
                    ByEmail = false;
                    ByName = false;
                    ByJobTitle = false;
                    ByPhone = false;
                    break;
                default:
                    return;
            }
        }
    }
}
