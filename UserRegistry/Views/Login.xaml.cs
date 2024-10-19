using System.Drawing;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;
using Newtonsoft.Json;
using UserRegistry.Models;
using UserRegistry.Utils;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Login
    {
        private List<Credentials> _credentialsList = [];
        private JsonModifier _credentialsManager = new();

        public Login()
        {
            this.InitializeComponent();
            ReadJSON();
        }


        private void LoginBtn(object sender, RoutedEventArgs e)
        {
            if (UserBox.IsChecked == true)
            {
                if (RegisterNewUser()) Frame.Navigate(typeof(Register), Username.Text);
            }
            else CheckCredentials();
        }

        private static async Task ErrorEffect(Control sender)
        {
            var defaultColor = sender.BorderBrush;
            sender.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Red);
            await Task.Delay(2000);
            sender.BorderBrush = defaultColor;
        }

        private bool RegisterNewUser()
        {
            var result = false;
            try
            {
                var encryptedPassword = EncryptMyPass(Password.Password);
                var doExists =  _credentialsList?.Exists(credential => credential.Username.Equals(Username.Text)) ?? false;

                if (doExists)
                {
                    ErrorDisplay.Text = "Unavailable username!";
                    return result;
                }
                
                _credentialsList?.Add(new Credentials(Username.Text, encryptedPassword));

                _credentialsManager.WriteCredentialsAsync(_credentialsList);

                Frame.Navigate(typeof(Register), Username.Text);
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
                        Frame.Navigate(typeof(Register), Username.Text);
                        doesExist = true;
                        break;
                    }

                    var usernameErrorEffect = ErrorEffect(Username);
                    var pasErrorEffect = ErrorEffect(Password);
                    ErrorDisplay.Text = "Invalid credentials";

                    await Task.WhenAll(usernameErrorEffect, pasErrorEffect);
                }

                if (!doesExist)
                {
                    ErrorDisplay.Text = "No user found";

                }

            }
            catch
            {
                ErrorDisplay.Text = "Error checking credentials";
            }

        }

        private async void ReadJSON()
        {
            try
            {
                _credentialsList = await _credentialsManager.ReadCredentialsAsync();
            }
            catch
            {
                ErrorDisplay.Text = "Error reading JSON";
            }
        }
    }
}