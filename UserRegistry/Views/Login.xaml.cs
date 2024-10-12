using System.Drawing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using System;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
        }


        private async void LoginBtn(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Equals("admin") && Password.Password.Equals("admin"))
            {
                Debug.WriteLine("Admin logged in at: " + DateTime.Now);
                Console.WriteLine("Admin logged in at: " + DateTime.Now);
                Frame.Navigate(typeof(Register));
            }
            else
            {
                var usernameErrorEffect = ErrorEffect(Username);
                var pasErrorEffect = ErrorEffect(Password);

                await Task.WhenAll(usernameErrorEffect, pasErrorEffect);
            }
        }

        private static async Task ErrorEffect(Control sender)
        {
            var defaultColor = sender.BorderBrush;
            sender.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
            await Task.Delay(2000);
            sender.BorderBrush = defaultColor;
        }
    }
}