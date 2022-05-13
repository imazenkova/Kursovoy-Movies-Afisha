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

namespace MoviesAfisha.ViewModels.AdminCinemaViewModels
{
    public class AddHallViewModel : BaseViewModel
    {
        public static string Username { get; set; }
        public int NameHall { get; set; }
        public int Capacity { get; set; }
        private RelayCommand addHall = null;
        public RelayCommand AddHall
        {
            get
            {
                return addHall ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;

                    string resultStr = "";

                    if (NameHall == 0)
                    {
                        SetRedBlockControll(window, "HallBox");
                    }
                    else if (Capacity == 0)
                    {
                        SetRedBlockControll(window, "CapacityBox");
                    }
                    else
                    {
                        UnitOfWork unitOfWork = new UnitOfWork();
                        Cinemas Cinema = new Cinemas();

                        var Cinemas = unitOfWork.Cinemas.GetAll();
                        foreach (Cinemas item in Cinemas)
                        {
                            if (Username == item.AdminCinema.Username)
                                Cinema = item;
                        }
                        Halls newHall = new Halls
                        {
                            IdCinema = Cinema.Id,
                            Name = this.NameHall,
                            Capacity = this.Capacity
                        };

                        resultStr = unitOfWork.Halls.Create(newHall);
                        MessageWindowCommand.ShowMessageToUser(resultStr);
                        ClearTextBox();
                        window.Close();
                    }

                });
            }
        }

        private void ClearTextBox()
        {
            NameHall = 0;
            Capacity = 0;
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
