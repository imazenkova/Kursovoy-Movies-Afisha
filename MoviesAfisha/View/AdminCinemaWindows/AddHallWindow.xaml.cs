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
    /// Логика взаимодействия для AddHallWindow.xaml
    /// </summary>
    public partial class AddHallWindow : Window
    {
        public AddHallWindow(string username)
        {
            InitializeComponent();
            AddHallViewModel.Username = username;
            DataContext = new AddHallViewModel();
        }
    }
}
