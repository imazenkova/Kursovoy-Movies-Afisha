using MoviesAfisha.View.UserWindows;
using MoviesAfisha.ViewModels.AdminViewModels;
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

namespace MoviesAfisha.View.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public static ListView AllUsers;
        public static ListView AllCinemas;
        public AdminWindow()
        {
            InitializeComponent();
            DataContext = new AdminViewModel();
            AllUsers = ViewAllUsers;
            AllCinemas = ViewAllCinemas;
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
