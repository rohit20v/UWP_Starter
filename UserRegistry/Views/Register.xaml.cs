using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UserRegistry.ViewModels;
using System.Linq;
using User = UserRegistry.Models.User;
using System.Diagnostics;
using System;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Register
    {
        private readonly UserViewModel _viewModel = new();

        public Register()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Welcome.Text = "Welcome " + e.Parameter as string;
        }


        private void AddUser(object sender, RoutedEventArgs e)
        {
            UserViewModel.Users.Add(_viewModel.GetUser);
            _viewModel.GetUser = new User();
            Debug.WriteLine($"Admin added User [{_viewModel.Name}] at: " + DateTime.Now);
            Console.WriteLine($"Admin added User [{_viewModel.Name}] at: " + DateTime.Now);
        }

        private void CheckInput(object sender, TextChangedEventArgs e)
        {
            BtnAddUser.IsEnabled = !string.IsNullOrEmpty(Username.Text) && !string.IsNullOrEmpty(UserSurname.Text);
        }


        private void CheckNumericInput(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            var newText = new string(textBox.Text.Where(char.IsDigit).ToArray());

            if (textBox.Text != newText)
            {
                textBox.Text = newText;
                textBox.SelectionStart = newText.Length;
            }

            if (textBox.Text.Length > textBox.MaxLength)
            {
                textBox.Text = textBox.Text.Substring(0, textBox.MaxLength);
                textBox.SelectionStart = textBox.MaxLength;
            }
        }
    }
}