using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
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
    public class UserInformationViewModel : BaseViewModel
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public OrderTickets SelectedTicket { get; set; }
        private string newUsername;
        public string NewUsername
        {
            get { return newUsername; }
            set
            {
                newUsername = value;
                NotifyPropertyChanged("NewUsername");
            }
        }
        public string User { get; set; }

        private List<OrderTickets> allOrderTickets;
        public List<OrderTickets> AllOrderTickets
        {
            get { return allOrderTickets; }
            set
            {
                allOrderTickets = value;
                NotifyPropertyChanged("AllOrderTickets");
            }
        }
        public UserInformationViewModel()
        {
            allOrderTickets = new UnitOfWork().OrderTicket.GetUserOrderTickets(Username);
        }
        private RelayCommand editUsername = null;
        public RelayCommand EditUsername
        {
            get
            {
                return editUsername ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;

                    string resultStr = "Неверный пароль";

                    if (NewUsername == null || NewUsername.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "UsernameBox");
                    }
                    else
                    {
                        List<User> allUsers = new UnitOfWork().User.GetAll();
                        bool isUser = false;
                        foreach (User user in allUsers)
                        {
                            if (Username == user.Username)
                            {
                                if (BCrypt.Net.BCrypt.Verify(Password, user.Password))
                                {
                                    User editUsernameUser = new User
                                    {
                                        Username = NewUsername,
                                        Password = BCrypt.Net.BCrypt.HashPassword(Password)
                                    };   
                                    UnitOfWork unitOfWork = new UnitOfWork();
                                    resultStr = unitOfWork.User.UpdateUsername(editUsernameUser,Username);
                                    MessageWindowCommand.ShowMessageToUser(resultStr);
                                    isUser = true;
                                    window.Close();
                                }

                            }
                        }
                        if(!isUser)
                            MessageWindowCommand.ShowMessageToUser(resultStr);
                    }
                    ClearTextBox();
                });
            }
        }

        private RelayCommand cancelTicket = null;
        public RelayCommand CancelTicket
        {
            get
            {
                return cancelTicket ?? new RelayCommand(obj =>
                {
                    string str;
                    try
                    {
                        if (SelectedTicket == null)
                        {
                            MessageWindowCommand.ShowMessageToUser("Выберите билет");
                        }
                        else
                        {
                            str = new UnitOfWork().OrderTicket.Update(SelectedTicket);
                            MessageWindowCommand.ShowMessageToUser(str);
                            UpdateAllOrderTickets();
                        }
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private void ClearTextBox()
        {
            Username = null;
            Password = null;
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
        private void UpdateAllOrderTickets()
        {
            AllOrderTickets = new UnitOfWork().OrderTicket.GetUserOrderTickets(Username);
            UserInformationWindow.AllOrderTickets.ItemsSource = null;
            UserInformationWindow.AllOrderTickets.Items.Clear();
            UserInformationWindow.AllOrderTickets.ItemsSource = AllOrderTickets;
            UserInformationWindow.AllOrderTickets.Items.Refresh();
        }
    }
}
