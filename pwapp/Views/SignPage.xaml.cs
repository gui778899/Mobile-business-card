using Firebase.Auth;
using PWApp.ViewModels;

namespace PWApp.Views;

public partial class SignPage : ContentPage
{
	public SignPage(FirebaseAuthClient client)
	{
		InitializeComponent();
		BindingContext = new SignPageViewModel(client);
    }
}