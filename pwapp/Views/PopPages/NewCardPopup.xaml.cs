using CommunityToolkit.Maui.Views;

namespace PWApp.Views.PopPages;

public partial class NewCardPopup : Popup
{
    private byte[] _data;
	public NewCardPopup(byte[] data)
	{
      
		InitializeComponent();  
        _data = data;
        qrCode.Source = ImageSource.FromStream(() => new MemoryStream(_data));
	}
    private async void OkayClicked(object sender, EventArgs e)
    {
        this.Close();
        await Shell.Current.GoToAsync($"//MainPage", true);
    }

    private void ScanClicked(object sender, EventArgs e)
    {
        this.Close(true);
    }
}