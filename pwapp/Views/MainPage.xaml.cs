using CommunityToolkit.Maui.Views;
using Firebase.Auth;
using Microsoft.Maui.Controls;
using PWApp.Theme;
using PWApp.ViewModels;

namespace PWApp;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}

