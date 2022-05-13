using MoviesAfisha.ViewModels.UserViewModels;
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

namespace MoviesAfisha.View.UserWindows
{
    /// <summary>
    /// Логика взаимодействия для UserInformationWindow.xaml
    /// </summary>
    public partial class UserInformationWindow : Window
    {
        public static ListBox AllOrderTickets;
        public UserInformationWindow()
        {
            InitializeComponent();
            DataContext = new UserInformationViewModel();
            AllOrderTickets = ViewSelectedTickets;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loginButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
