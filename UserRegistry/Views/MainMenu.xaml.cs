using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMenu : Page
    {
        private string _loggedUser;

        public MainMenu()
        {
            this.InitializeComponent();
        }

        private void LoadDefaultPage(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(Register), _loggedUser);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _loggedUser = e.Parameter as string;
        }

        //TODO: FIX THE NAV FUNCTIONALITY
        private void ChangePage(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = args.SelectedItem as NavigationViewItem;

            switch (item.Tag.ToString())
            {
                case "Register":
                    if (_loggedUser == "")
                    {
                        ContentFrame.Navigate(typeof(Login));
                        break;
                    }

                    ContentFrame.Navigate(typeof(Register), _loggedUser);
                    break;
                case "Exit":
                    CoreApplication.Exit();
                    break;
            }
        }
    }
}