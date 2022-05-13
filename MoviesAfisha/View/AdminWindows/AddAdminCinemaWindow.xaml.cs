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
    /// Логика взаимодействия для AddAdminCinemaWindow.xaml
    /// </summary>
    public partial class AddAdminCinemaWindow : Window
    {
        public AddAdminCinemaWindow()
        {
            InitializeComponent();
            DataContext = new AddAdminCinemaViewModel();
        }
    }
}
