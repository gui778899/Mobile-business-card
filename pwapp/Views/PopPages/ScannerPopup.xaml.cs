using CommunityToolkit.Maui.Views;

namespace PWApp.Views.PopPages;

public partial class ScannerPopPage : Popup
{
	public ScannerPopPage()
	{
        InitializeComponent();
	}

    private async void OkayClicked(object sender, EventArgs e)
    {
		await this.CloseAsync();
        await Shell.Current.GoToAsync($"//MainPage", true);
    }

    private void ScanClicked(object sender, EventArgs e)
    {
        this.CloseAsync(true);
    }
}