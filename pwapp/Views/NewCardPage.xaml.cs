using PWApp.ViewModels;

namespace PWApp;

public partial class NewCardPage : ContentPage
{
	public NewCardPage(NewCardPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}