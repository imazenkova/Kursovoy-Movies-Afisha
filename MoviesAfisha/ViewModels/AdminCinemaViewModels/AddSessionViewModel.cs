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
    public class AddSessionViewModel:BaseViewModel
    {
        public static string Username { get; set; }
        public Film Film { get; set; }
        public Halls Hall { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int Price { get; set; }

        private List<Film> allFilms;
        public List<Film> AllFilms
        {
            get { return allFilms; }
            set
            {
                allFilms = value;
                NotifyPropertyChanged("AllFilms");
            }
        }
        private List<Halls> allHalls;
        public List<Halls> AllHalls
        {
            get { return allHalls; }
            set
            {
                allHalls = (from hall in value
                              where hall.Cinemas.AdminCinema.Username == Username
                              select hall).ToList();
                NotifyPropertyChanged("AllHalls");
            }
        }

        public AddSessionViewModel()
        {
            allFilms = new UnitOfWork().Film.GetAll();
            allHalls = new UnitOfWork().Halls.GetAll(Username);
        }

        private RelayCommand addSession = null;
        public RelayCommand AddSession // Кнопка дообавления сеанса
        {
            get
            {
                return addSession ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;
                    string resultStr = "";
                    int place = 10;

                    if (Film == null)
                    {
                        SetRedBlockControll(window, "FilmsBox");
                    }
                    else if (Hall == null)
                    {
                        SetRedBlockControll(window, "HallsBox");
                    }
                    else if (Date == null || Date.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "DateBox");
                    }
                    else if (Time == null || Time.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "TimeBox");
                    }
                    else if (Price == 0)
                    {
                        SetRedBlockControll(window, "PriceBox");
                    }
                    else
                    {
                        Sessions newSessions = new Sessions
                        {
                            IdFilm = this.Film.IdFilm,
                            IdHall = this.Hall.Id,
                            Time = this.Time,
                            Date = this.Date
                        };
                        UnitOfWork unitOfWork = new UnitOfWork();
                        resultStr = unitOfWork.Session.Create(newSessions);

                        Sessions idSessions = unitOfWork.Session.GetAll().LastOrDefault();
                        for (int i = 1; i <= (Hall.Capacity / place); i++)
                        {
                            for (int j = 1; j <= place; j++)
                            {
                                Tickets ticket = new Tickets
                                {
                                    IDSession = idSessions.Id,
                                    Row = i,
                                    Place = j,
                                    Price = this.Price,
                                    Availability = true
                                };
                                resultStr = unitOfWork.Ticket.Create(ticket);
                            }
                        }

                        MessageWindowCommand.ShowMessageToUser(resultStr);
                        ClearTextBox();
                        window.Close();
                    }

                });
            }
        }
        private void ClearTextBox()
        {
            Film = null;
            Hall = null;
            Time = null;
            Date = null;
            Price = 0;
        }

        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
