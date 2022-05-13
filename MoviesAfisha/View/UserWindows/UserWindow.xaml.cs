using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.ViewModels.UserViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MoviesAfisha.View.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow(string username)
        {
            InitializeComponent();
            DataContext = new UserViewModel(username);
            OrderTicketsViewModel.Username = username;
            UserInformationViewModel.Username = username;
        }

        private void LoginWindowOpen(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            LoginWindow loginwindow = new LoginWindow();
            loginwindow.Owner = Application.Current.MainWindow;
            window.Hide();
            loginwindow.ShowDialog();
            window.Close();
        }

        private void OpenOrderTisketsWindow(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            Film film = (Film)((ListBoxItem)ViewAllFilms.ContainerFromElement((Button)sender)).Content;
            OrderTicketsWindow orderTicketsWindow = new OrderTicketsWindow(film);
            orderTicketsWindow.Owner = Application.Current.MainWindow;
            orderTicketsWindow.ShowDialog();
        }

        private void ViewAllFilms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
