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
    /// Логика взаимодействия для OrderTicketsWindow.xaml
    /// </summary>
    public partial class OrderTicketsWindow : Window
    {
        public static ListBox Tickets;
        public OrderTicketsWindow(Film film)
        {
            InitializeComponent();
            OrderTicketsViewModel.SelectedFilm = film;
            DataContext = new OrderTicketsViewModel();
        }

        private void ShowTickets(object sender, RoutedEventArgs e)
        {
            //Cinemas cinema = (Cinemas)((ListBoxItem)ViewSelectedCinema.ContainerFromElement((Button)sender)).Content;
            
        }
    }
}
