using MoviesAfisha.View.UserWindows;
using MoviesAfisha.ViewModels.AdminCinemaViewModels;
using System;
using System.Collections.Generic;
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

namespace MoviesAfisha.View.AdminCinemaWindows
{
    /// <summary>
    /// Логика взаимодействия для AdminCinemaWindow.xaml
    /// </summary>
    public partial class AdminCinemaWindow : Window
    {
        public static ListView AllFilms;
        public static ListView AllSessions;
        public static ListView AllOrderTickets;
        public AdminCinemaWindow(string username)
        {
            InitializeComponent();
            AdminCinemaViewModel.Username = username;
            DataContext = new AdminCinemaViewModel();
            
            AllFilms = ViewAllFilms;
            AllSessions = ViewAllSessions;
            AllOrderTickets = ViewAllOrderTickets;
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
    }
}
