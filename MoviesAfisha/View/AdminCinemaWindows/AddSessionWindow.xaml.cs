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
    /// Логика взаимодействия для AddSessionWindow.xaml
    /// </summary>
    public partial class AddSessionWindow : Window
    {
        public AddSessionWindow(string username)
        {
            InitializeComponent();
            AddSessionViewModel.Username = username;
            DataContext = new AddSessionViewModel();
        }
    }
}
