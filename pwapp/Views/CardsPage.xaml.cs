using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics.Text;
using Newtonsoft.Json;
using PWApp.Data;
using PWApp.Data.Models;
using PWApp.Views.PopPages;
using PWApp.ViewModels;
using System.Windows.Input;

namespace PWApp;

public partial class CardsPage : ContentPage
{

    public CardsPage(CardsPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}