using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.View;
using MoviesAfisha.View.AdminWindows;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoviesAfisha.ViewModels.AdminViewModels
{
    public class AddUserViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public static string Password { get; set; }

        private RelayCommand addUser = null;
        public RelayCommand AddUser
        {
            get
            {
                return addUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";

                    if (Username == null || Username.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "NameBox");
                    }
                    else if (Password == null || Password.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "PasswordBox");
                    }
                    else
                    {
                        User newUser = new User
                        {
                            Username = this.Username,
                            Password = BCrypt.Net.BCrypt.HashPassword(Password)
                        };
                        UnitOfWork unitOfWork = new UnitOfWork();
                        resultStr = unitOfWork.User.Create(newUser);

                        MessageWindowCommand.ShowMessageToUser(resultStr);
                        ClearTextBox();
                        window.Close();
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
    }
}
