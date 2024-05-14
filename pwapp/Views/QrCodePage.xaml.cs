using Camera.MAUI;
using CommunityToolkit.Maui.Views;
using Newtonsoft.Json;
using PWApp.Data.Models;
using PWApp.Views.PopPages;
using PWApp.ViewModels;
using System;
using System.Text;
using PWApp.Data.Services;
using pwapp;

namespace PWApp;

public partial class QrCodePage : ContentPage
{
    private readonly DataService _service;
    public QrCodePage(QrCodePageViewModel vm , DataService service)
    {
        InitializeComponent();
        BindingContext = vm;
        _service = service;
        qrScanner.Options = new ZXing.Net.Maui.BarcodeReaderOptions()
        {
            AutoRotate = true,
            Formats = ZXing.Net.Maui.BarcodeFormat.QrCode,
            Multiple = false,
            TryHarder = true,
            TryInverted = false
        };
    }
    
    private async void qrScanner_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        qrScanner.IsDetecting = false;
        try
        {
            await Task.Run(() =>
            {
                var result = e.Results[0].Value.ToString();
                CustomerModel model = JsonConvert.DeserializeObject<CustomerModel>(result);
                model.CreateBy = Preferences.Get("user_key", string.Empty);
                model.IsMyData = false;
                try
                {
                    var rows =  _service.AddCard(model);
                    MainThread.BeginInvokeOnMainThread(new Action(() =>
                    {
                        App.Current.MainPage.ShowPopup(new ScannerPopPage());
                    }));
                }
                catch (SQLite.SQLiteException ex)
                {
                    MainThread.BeginInvokeOnMainThread(() => { errorLabel.Text = "You can't scan already added cards"; }) ;
                }
            });
        }
        catch
        {
            // MainThread.BeginInvokeOnMainThread(() => { ErrorText = "Please scan correct QR-code!"; });
        }
        qrScanner.IsDetecting = true;
    }
}