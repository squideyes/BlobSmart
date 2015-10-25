using Microsoft.Azure.AppService;
using Newtonsoft.Json;
using System;
using System.Windows;

namespace BlobSmart.LoginTest
{
    public partial class MainWindow : Window
    {
        private const string GATEWAY_URL =
            "http://blobsmartdc94f3ac282e4ce8a52d0e4e5ce313d3.azurewebsites.net/";

        private const string URL_TOKEN = "#token=";

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            browser.LoadCompleted += (s, e) =>
            {
                if (e.Uri.AbsoluteUri.IndexOf(URL_TOKEN) == -1)
                    return;

                try
                {
                    var encodedJson = e.Uri.AbsoluteUri.Substring(
                        e.Uri.AbsoluteUri.IndexOf(URL_TOKEN) + URL_TOKEN.Length);

                    var decodedJson = Uri.UnescapeDataString(encodedJson);

                    var result = JsonConvert.DeserializeObject<dynamic>(decodedJson);

                    string userId = result.user.userId;

                    string userToken = result.authenticationToken;

                    var appServiceClient = new AppServiceClient(GATEWAY_URL);

                    appServiceClient.SetCurrentUser(userId, userToken);

                    var api = appServiceClient.CreateBlobSmartAPI();

                    var values = api.Values.Get();

                    foreach (var value in values)
                        textBox.Text += value + Environment.NewLine;

                    //appServiceClient.Logout();
                    //browser.Navigate(string.Format(@"{0}login/aad", GATEWAY_URL));
                }
                catch (Exception error)
                {
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            browser.Navigate(string.Format(@"{0}login/aad", GATEWAY_URL));
        }
    }
}
