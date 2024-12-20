﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UserRegistry.ViewModels;
using System.Linq;
using User = UserRegistry.Models.User;
using System.Diagnostics;
using System;
using Windows.UI.Xaml.Navigation;
using UserRegistry.Utils;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    public sealed partial class Register
    {
        private readonly UserViewModel _viewModel = new();
        private JsonModifier _usersManager = new();
        private string _loggedUser = "";
        private int _lastUserId;

        public Register()
        {
            InitializeComponent();
            DataContext = _viewModel;
            DatePicker.MinYear = new DateTimeOffset(new DateTime(1950, 1, 1));
            DatePicker.MaxYear = DateTimeOffset.Now;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _loggedUser = e.Parameter as string;
            LoadUsersIntoCollection();
        }


        private void AddUser(object sender, RoutedEventArgs e)
        {
            _lastUserId += 1;
            _viewModel.GetUser.UserId = _lastUserId;
            UserViewModel.Users.Add(_viewModel.GetUser);
            JsonModifier.WriteJsonFile(UserViewModel.Users.ToList(), $"{_loggedUser}.json");
            _viewModel.GetUser = new User();
            Debug.WriteLine($"Admin added User [{_viewModel.Name}] at: " + DateTime.Now);
            Console.WriteLine($"Admin added User [{_viewModel.Name}] at: " + DateTime.Now);
        }

        private void UpdateUserOnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.UpdateSelectedUser();
            JsonModifier.WriteJsonFile(UserViewModel.Users.ToList(), $"{_loggedUser}.json");
            _viewModel.GetUser = new User();
            BtnUpdatedUser.IsEnabled = false;
            BtnDeleteUser.IsEnabled = false;
        }
        private void SelectedUser(object sender, SelectionChangedEventArgs e)
        {
            BtnUpdatedUser.IsEnabled = true;
            BtnDeleteUser.IsEnabled = true;
        }

        private void CheckInput(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            var newText = new string(textBox.Text.Where(char.IsLetter).ToArray());

            if (textBox.Text != newText)
            {
                textBox.Text = newText;
                textBox.SelectionStart = newText.Length;
            }
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

        private void LoadUsersIntoCollection()
        {
            UserViewModel.Users.Clear();
            try
            {
                foreach (var item in JsonModifier.ReadJsonFile<User>($"{_loggedUser}.json"))
                {
                    UserViewModel.Users.Add(item);
                }

                var lastUser = UserViewModel.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
                _lastUserId = lastUser?.UserId ?? 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void RemoveUserOnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteSelectedUser();
            JsonModifier.WriteJsonFile(UserViewModel.Users.ToList(), $"{_loggedUser}.json");
            _viewModel.GetUser = new User();
            BtnDeleteUser.IsEnabled = false;
            BtnUpdatedUser.IsEnabled = false;
        }
    }
}