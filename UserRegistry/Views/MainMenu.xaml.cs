using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UserRegistry.Utils;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu
    {
        private string _loggedUser;

        public MainMenu()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _loggedUser = e.Parameter as string;
            Welcome.Text = $"Hi, {_loggedUser}!";
            ContentFrame.Navigate(typeof(Home), _loggedUser);
            UpdateButtonStates();
            
        }


        private void GoToHome(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Home), _loggedUser);
            UpdateButtonStates();

        }

        private void GoToRegister(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Register), _loggedUser);
            UpdateButtonStates();

        }

        private void UpdateButtonStates()
        {
            BtnHome.IsEnabled = ContentFrame.CurrentSourcePageType != typeof(Home);
            BtnRegister.IsEnabled = ContentFrame.CurrentSourcePageType != typeof(Register);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            _loggedUser = "";
            JsonModifier.WriteJsonFile([""], "session.json");
            Frame.Navigate(typeof(Login));
        }
    }
}