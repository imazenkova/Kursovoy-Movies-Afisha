using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MoviesAfisha.ViewModels.AdminViewModels
{
    public class EditUserViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string NewPassword { get; set; }
        private RelayCommand editPasswordUser = null;
        public RelayCommand EdiPasswordtUser
        {
            get
            {
                return editPasswordUser ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "";

                    if (Username == null || Username.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "NameBox");
                    }
                    else if (NewPassword == null || NewPassword.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "PasswordBox");
                    }
                    else
                    {
                        User editPasswordUser = new User
                        {
                            Username = this.Username,
                            Password = BCrypt.Net.BCrypt.HashPassword(NewPassword)
                        };

                        UnitOfWork unitOfWork = new UnitOfWork();
                        resultStr = unitOfWork.User.Update(editPasswordUser);

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
            NewPassword = null;
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

    }
}
