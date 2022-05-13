using MoviesAfisha.Models.DataBaseModels;
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
    /// Логика взаимодействия для AddOrderTicketWindow.xaml
    /// </summary>
    public partial class AddOrderTicketWindow : Window
    {
        public AddOrderTicketWindow(Tickets ticket,string username)
        {
            InitializeComponent();
            AddOrderTicketViewModel.Username = username;
            AddOrderTicketViewModel.Ticket = ticket;
            DataContext = new AddOrderTicketViewModel();
        }
    }
}
