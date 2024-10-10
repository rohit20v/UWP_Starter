using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UserRegistry.Models;
using UserRegistry.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Register : Page
    {
        private readonly UserViewModel _viewModel = new();

        public Register()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }


        private void AddUser(object sender, RoutedEventArgs e)
        {
            UserViewModel.Users.Add(_viewModel.GetUser);
            _viewModel.GetUser = new User();
        }

        private void CheckInput(object sender, TextChangedEventArgs e)
        {
            BtnAddUser.IsEnabled = !string.IsNullOrEmpty(Username.Text) && !string.IsNullOrEmpty(UserSurname.Text);
        }
    }
}