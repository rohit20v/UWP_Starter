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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Login
    {
        private List<Credentials> _credentialsList = [];

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
            bool result = false;
            try
            {
                var encryptedPassword = EncryptMyPass(Password.Password);

                _credentialsList.Add(new(Username.Text, encryptedPassword));

                var file = ApplicationData.Current.LocalFolder
                    .CreateFileAsync("credentials.json", CreationCollisionOption.ReplaceExisting)
                    .GetAwaiter()
                    .GetResult();

                FileIO.WriteTextAsync(file,
                    JsonConvert.SerializeObject(_credentialsList)
                ).GetAwaiter().GetResult();
                Frame.Navigate(typeof(Register), Username.Text);
            }
            catch (Exception ex)
            {
            }

            return result;
        }

        private string EncryptMyPass(string password)
        {
            string hashedPass = "";
            SHA256 sha256 = SHA256.Create();
            byte[] bteBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var item in bteBytes)
            {
                hashedPass += item.ToString("x2");
            }

            return hashedPass;
        }

        private async void CheckCredentials()
        {
            bool isValid = false;

            foreach (var item in _credentialsList)
            {
                //TODO make the login work
                isValid = (item.Username.ToUpper().Equals(Username.Text.ToUpper()) &&
                           item.Password.Equals(Password.Password));
                if (isValid)
                {
                    Debug.WriteLine("Admin logged in at: " + DateTime.Now);
                    Console.WriteLine("Admin logged in at: " + DateTime.Now);
                    Frame.Navigate(typeof(Register), Username.Text);
                    break;
                }
                else
                {
                    var usernameErrorEffect = ErrorEffect(Username);
                    var pasErrorEffect = ErrorEffect(Password);

                    await Task.WhenAll(usernameErrorEffect, pasErrorEffect);
                }
            }
        }

        private async void ReadJSON()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("credentials.json");
                string cred = await FileIO.ReadTextAsync(file);
                _credentialsList = JsonConvert.DeserializeObject<List<Credentials>>(cred);
            }
            catch
            {

            }
        }
    }
}