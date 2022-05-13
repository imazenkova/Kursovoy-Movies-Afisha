using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.View.AdminWindows;
using MoviesAfisha.View.UserWindows;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoviesAfisha.ViewModels.UserViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string PasswordUser { get; set; }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged("Password");
            }
        }


        //private RelayCommand login = null;
        //public RelayCommand Login
        //{
        //    get
        //    {
        //        //return login ?? new RelayCommand(obj =>
        //        //{
        //        //    Window window = obj as Window;

        //        //    List<User> allUsers = DataBaseModel.GetAllUsers();
        //        //    List<AdminCinema> allAdminCinemas = DataBaseModel.GetAllAdminCinema();
        //        //    List<Admin> admin = DataBaseModel.GetAdmin();

        //        //    if (Username == null || Username.Replace(" ", "").Length == 0)
        //        //    {
        //        //        MessageWindowCommand.ShowMessageToUser("Введите имя пользователя");
        //        //    }
        //        //    //else if (PasswordUser == null || PasswordUser.Replace(" ", "").Length == 0)
        //        //    //{
        //        //    //    MessageWindowCommand.ShowMessageToUser("Введите пароль");
        //        //    //}
        //        //    else
        //        //    {
        //        //        if (true) //если админ
        //        //        {
        //        //            AdminWindow adminWindow = new AdminWindow
        //        //            {
        //        //                WindowStartupLocation = WindowStartupLocation.CenterScreen
        //        //            };
        //        //            adminWindow.Owner = Application.Current.MainWindow;
        //        //            window.Hide();
        //        //            adminWindow.ShowDialog();
        //        //            window.Close();
        //        //        }

        //        //        foreach (User user in allUsers)
        //        //        {
        //        //            if (Username == user.Username)
        //        //            {
        //        //                //проверка пароля
        //        //                //открытие окна
        //        //            }
        //        //        }

        //        //        foreach (AdminCinema adminCinema in allAdminCinemas)
        //        //        {
        //        //            if (Username == adminCinema.Username)
        //        //            {
        //        //                //проверка пароля
        //        //                //открытие окна
        //        //            }
        //        //        }
        //        //    }
        //        //});
        //    }
        //}

        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
