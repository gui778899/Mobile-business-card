using Firebase.Auth;
using PWApp;
using PWApp.Views;

namespace pwapp
{
    public partial class App : Application
    {
        public static bool IsDark { get; set; } = true;
        public App(FirebaseAuthClient client)
        {
            InitializeComponent();

            if (Preferences.ContainsKey("remembered"))
            {
                MainPage = new AppShell();
            }
            else
            {
                Preferences.Clear();
                MainPage = new SignPage(client);
            }
        }
    }
}
