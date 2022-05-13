using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.View.AdminCinemaWindows;
using MoviesAfisha.View.AdminWindows;
using MoviesAfisha.ViewModels.AdminViewModels;
using MoviesAfisha.ViewModels.Commands;
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
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        #region login
        private void LoginButton(object sender, RoutedEventArgs e)
        {
            Window window = GetWindow(this);
            try
            {
                List<User> allUsers = new UnitOfWork().User.GetAll();
                List<AdminCinema> allAdminCinemas = new UnitOfWork().AdminCinema.GetAll();
                List<Admin> admin;
                using (DataBaseContext db = new DataBaseContext())
                {
                    admin = db.Admin.ToList();
                }

                bool isUser = false; ;
                bool isAdminCinema = false;

                string Username = UsernameLogin.Text;
                string Password = PasswordLogin.Password;

                if (Username == "" || Username.Replace(" ", "").Length == 0)
                {
                    MessageWindowCommand.ShowMessageToUser("Введите имя пользователя");
                }
                else if (Password == "" || Password.Replace(" ", "").Length == 0)
                {
                    MessageWindowCommand.ShowMessageToUser("Введите пароль");
                }
                else
                {
                    #region if your are admin
                    if (Username == admin[0].Username)
                    {
                        if (BCrypt.Net.BCrypt.Verify(Password, admin[0].Password))
                        {
                            AdminWindow adminWindow = new AdminWindow
                            {
                                WindowStartupLocation = WindowStartupLocation.CenterScreen
                            };
                            window.Hide();
                            adminWindow.ShowDialog();
                            window.Close();
                        }
                    }
                    #endregion

                    #region if your are user
                    foreach (User user in allUsers)
                    {
                        if (Username == user.Username)
                        {
                            if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
                                isUser = true;
                        }
                    }

                    foreach (AdminCinema adminCinema in allAdminCinemas)
                    {
                        if (Username == adminCinema.Username)
                        {
                            if (BCrypt.Net.BCrypt.Verify(Password, adminCinema.Password))
                                isAdminCinema = true;
                        }
                    }

                    if (isUser)
                    {
                        UserWindow userWindow = new UserWindow(Username)
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        window.Hide();
                        userWindow.ShowDialog();
                        window.Close();
                    }
                    else if (isAdminCinema)
                    {
                        AdminCinemaWindow adminCinemaWindow = new AdminCinemaWindow(Username)
                        {
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };
                        window.Hide();
                        adminCinemaWindow.ShowDialog();
                        window.Close();
                    }
                    else if (!isAdminCinema || !isUser)
                    {
                        MessageWindowCommand.ShowMessageToUser("Неверное имя пользователя или пароль");
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageWindowCommand.ShowMessageToUser(ex.Message);
            }
        }
        #endregion

        #region register
        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            try { 
                string Username = UsernameRegister.Text;
                string Password = PasswordRegister.Password;
                string resultStr = "";
                if (Username == "" || Username.Replace(" ", "").Length == 0)
                {
                    MessageWindowCommand.ShowMessageToUser("Введите имя пользователя");
                }
                else if (Password == "" || Password.Replace(" ", "").Length == 0)
                {
                    MessageWindowCommand.ShowMessageToUser("Введите пароль");
                }
                else
                {
                    UnitOfWork unitOfWork = new UnitOfWork();
                    User user = new User
                    {
                        Username = Username,
                        Password = BCrypt.Net.BCrypt.HashPassword(Password)
                    };
                    resultStr = unitOfWork.User.RegisterUser(user);

                    MessageWindowCommand.ShowMessageToUser(resultStr);
                }
                if (resultStr.Equals("Сделано!\nТеперь вы можете зайти в аккаунт."))
                {
                    registerWindow.Visibility = Visibility.Collapsed;
                    loginWindow.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageWindowCommand.ShowMessageToUser(ex.Message);
            }
        }
        #endregion

        #region buttons
        private void CloseLoginWindow(object sender, RoutedEventArgs e)
        {
            loginWindow.Visibility = Visibility.Collapsed;
            registerWindow.Visibility = Visibility.Visible;
            UsernameLogin.Clear();
            PasswordLogin.Clear();
            UsernameRegister.Clear();
            PasswordRegister.Clear();
        }

        private void CloseRegisterWindow(object sender, MouseButtonEventArgs e)
        {
            loginWindow.Visibility = Visibility.Visible;
            registerWindow.Visibility = Visibility.Collapsed;
            UsernameRegister.Clear();
            PasswordRegister.Clear();
            UsernameLogin.Clear();
            PasswordLogin.Clear();
        }

        private void LabelLoginEnter(object sender, MouseEventArgs e)
        {
            LabelLogin.Foreground = Brushes.DarkGray;
        }

        private void LabelLoginLeave(object sender, MouseEventArgs e)
        {
            LabelLogin.Foreground = Brushes.White;
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        #endregion

        private void UsernameLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
