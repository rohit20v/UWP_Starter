using System.Drawing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UserRegistry.Models;
using UserRegistry.Utils;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Login
    {
        private const string Filename = "credentials.json";
        private List<Credentials> _credentialsList = [];

        public Login()
        {
            this.InitializeComponent();
            ReadJson();
            this.Loaded += (s, e) => CheckSession();
        }


        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            if (UserBox.IsChecked == true)
            {
                if (RegisterNewUser()) Frame.Navigate(typeof(MainMenu), Username.Text);
            }
            else CheckCredentials();
        }

        private static async Task ErrorEffect(Control sender, Brush brushColor)
        {
            var defaultColor = sender.BorderBrush;
            sender.BorderBrush = brushColor;
            await Task.Delay(2000);
            sender.BorderBrush = defaultColor;
        }

        private bool RegisterNewUser()
        {
            var result = false;
            try
            {
                var encryptedPassword = EncryptMyPass(Password.Password);
                var doExists = _credentialsList?.Exists(credential => credential.Username.Equals(Username.Text)) ??
                               false;

                if (doExists)
                {
                    ErrorDisplay.Text = "Unavailable username!";
                    return result;
                }

                _credentialsList?.Add(new Credentials(Username.Text, encryptedPassword));

                JsonModifier.WriteJsonFile(_credentialsList, Filename);

                JsonModifier.WriteJsonFile(
                    [
                        new
                        {
                            Credentials = new Credentials(Username.Text, encryptedPassword),
                            RememberMe = RememberMe.IsChecked
                        }
                    ],
                    "session.json");

                Frame.Navigate(typeof(MainMenu), Username.Text);
                result = true;
            }
            catch (Exception ex)
            {
                ErrorDisplay.Text = "Error registering new user";
            }

            return result;
        }

        private string EncryptMyPass(string password)
        {
            StringBuilder hashedPass = new StringBuilder();
            SHA256 sha256 = SHA256.Create();
            byte[] bteBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var item in bteBytes)
            {
                hashedPass.Append(item.ToString("x2"));
            }

            return hashedPass.ToString();
        }

        private async void CheckCredentials()
        {
            var doesExist = false;

            try
            {
                foreach (var item in _credentialsList)
                {
                    var hashedPass = EncryptMyPass(Password.Password);
                    var isValid = (item.Username.Equals(Username.Text) &&
                                   item.Password.Equals(hashedPass));
                    if (isValid)
                    {
                        Debug.WriteLine("Admin logged in at: " + DateTime.Now);
                        Console.WriteLine("Admin logged in at: " + DateTime.Now);
                        JsonModifier.WriteJsonFile(
                            [item.Username, RememberMe.IsChecked == true ? "true" : "false"],
                            "session.json");

                        Frame.Navigate(typeof(MainMenu), Username.Text);
                        doesExist = true;
                        break;
                    }

                    var usernameErrorEffect = ErrorEffect(Username, new SolidColorBrush(Windows.UI.Colors.LawnGreen));
                    var pasErrorEffect = ErrorEffect(Password, new SolidColorBrush(Windows.UI.Colors.LawnGreen));
                    ErrorDisplay.Text = "Loading...";
                    ErrorDisplay.Foreground = new SolidColorBrush(Windows.UI.Colors.LawnGreen);

                    await Task.WhenAll(usernameErrorEffect, pasErrorEffect);
                }

                if (!doesExist)
                {
                    var usernameErrorEffect = ErrorEffect(Username, new SolidColorBrush(Windows.UI.Colors.Red));
                    var pasErrorEffect = ErrorEffect(Password, new SolidColorBrush(Windows.UI.Colors.Red));
                    await Task.WhenAll(usernameErrorEffect, pasErrorEffect);

                    ErrorDisplay.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                    ErrorDisplay.Text = "No user found";
                }
            }
            catch
            {
                var usernameErrorEffect = ErrorEffect(Username, new SolidColorBrush(Windows.UI.Colors.Red));
                var pasErrorEffect = ErrorEffect(Password, new SolidColorBrush(Windows.UI.Colors.Red));
                await Task.WhenAll(usernameErrorEffect, pasErrorEffect);
                ErrorDisplay.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

                ErrorDisplay.Text = "Error checking credentials";
            }
        }

        private void ReadJson()
        {
            try
            {
                _credentialsList = JsonModifier.ReadJsonFile<Credentials>(Filename);
            }
            catch
            {
                var usernameErrorEffect = ErrorEffect(Username, new SolidColorBrush(Windows.UI.Colors.Red));
                var pasErrorEffect = ErrorEffect(Password, new SolidColorBrush(Windows.UI.Colors.Red));
                Task.WhenAll(usernameErrorEffect, pasErrorEffect).GetAwaiter().GetResult();
                ErrorDisplay.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);

                ErrorDisplay.Text = "Error reading JSON";
            }
        }

        public void CheckSession()
        {
            List<string> session = JsonModifier.ReadJsonFile<string>("session.json");
            Debug.WriteLine(session.Count);
            try
            {
                if (session.Count() == 2)
                {
                    var username = session[0];
                    var isTrue = session[1];
                    if (isTrue.Equals("true"))
                    {
                        Frame.Navigate(typeof(MainMenu), username);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("No session found");
            }
        }
    }
}