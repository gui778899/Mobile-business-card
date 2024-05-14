using PWApp.Data.Models;
using PWApp.ViewModels;

namespace PWApp;

public partial class ChangeCardPage : ContentPage
{
	public ChangeCardPage(ChangeCardPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}