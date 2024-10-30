using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UserRegistry.Models;
using UserRegistry.Utils;
using UserRegistry.ViewModels;
using DataTemplate = Windows.UI.Xaml.DataTemplate;
using User = UserRegistry.Models.UserAPI.User;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UserRegistry.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HttpFetch : Page
    {
        private const string _url = "https://jsonplaceholder.typicode.com/";

        public HttpFetch()
        {
            this.InitializeComponent();
            FetchDefaultJsonData();
        }

        public void FetchDefaultJsonData()
        {
            CommentList.ItemTemplate = this.Resources["CommentTemplate"] as DataTemplate;

            CommentList.ItemsSource = HttpRequest.FetchApi<Comment>(_url + "comments");
        }

        private void FetchComments(object sender, RoutedEventArgs e)
        {
            FetchDefaultJsonData();
        }

        private void FetchTodo(object sender, RoutedEventArgs e)
        {
            CommentList.ItemTemplate = this.Resources["TodoTemplate"] as DataTemplate;

            CommentList.ItemsSource = HttpRequest.FetchApi<Todo>(_url + "todos");
        }
        private void FetchUser(object sender, RoutedEventArgs e)
        {
            CommentList.ItemTemplate = this.Resources["UsersTemplate"] as DataTemplate;

            CommentList.ItemsSource = HttpRequest.FetchApi<User>(_url + "users");
        }
    }
}