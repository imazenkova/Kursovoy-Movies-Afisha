using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.View;
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
    public class AddAdminCinemaViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Cinema { get; set; }
        public string Address { get; set; }
        public AdminCinema AdminCinema { get; set; }

        private RelayCommand addAdminCinema = null;
        public RelayCommand AddAdminCinema
        {
            get
            {
                return addAdminCinema ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;

                    string resultStr = "";

                    if (Username == null || Username.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "NameBlock");
                    }
                    else if (Password == null || Password.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "PasswordBox");
                    }
                    else if (Cinema == null || Cinema.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "NameCinemaBox");
                    }
                    else if (Address == null || Address.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "AddressBox");
                    }
                    else
                    {
                        AdminCinema newAdminCinema = new AdminCinema
                        {
                            Username = this.Username,
                            Password = BCrypt.Net.BCrypt.HashPassword(this.Password)
                        };
                        UnitOfWork unitOfWork = new UnitOfWork();
                        resultStr = unitOfWork.AdminCinema.Create(newAdminCinema);
                       
                        AdminCinema idAdminCinema = unitOfWork.AdminCinema.GetAll().LastOrDefault();

                        Cinemas newCinema = new Cinemas
                        {
                            Name = this.Cinema,
                            Address = this.Address,
                            IdAdmin = idAdminCinema.Id
                        };

                        resultStr = unitOfWork.Cinemas.Create(newCinema);

                        Cinemas idCinema = unitOfWork.Cinemas.GetAll().LastOrDefault();
                        Halls hall = new Halls
                        {
                            IdCinema = idCinema.Id,
                            Name = 1,
                            Capacity = 100
                        };

                        resultStr = unitOfWork.Halls.Create(hall);

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
            Cinema = null;
            Address = null;
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
