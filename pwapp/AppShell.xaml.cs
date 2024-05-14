namespace Pwapp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"//MainPage/{nameof(NewCardPage)}",
            typeof(NewCardPage));
            Routing.RegisterRoute($"//MainPage/{nameof(CardsPage)}",
            typeof(CardsPage));
            Routing.RegisterRoute($"//MainPage/{nameof(QrCodePage)}",
            typeof(QrCodePage));
            Routing.RegisterRoute($"//MainPage/{nameof(ChangeCardPage)}", 
            typeof(ChangeCardPage));

        }
    }
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await this.GoToAsync($"//MainPage/", true);
    }
}
