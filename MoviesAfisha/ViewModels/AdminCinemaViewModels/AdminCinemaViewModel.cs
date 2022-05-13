using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using MoviesAfisha.View.AdminCinemaWindows;
using MoviesAfisha.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoviesAfisha.ViewModels.AdminCinemaViewModels
{
    public class AdminCinemaViewModel:BaseViewModel
    {
        public TabItem SelectedTabItem { get; set; }
        public Film SelectedFilm { get; set; }
        public static string Username { get; set; }

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

        public List<Tickets> allTickets;
        public List<Tickets> AllTickets
        {
            get { return allTickets; }
            set
            {
                allTickets = value;
                NotifyPropertyChanged("AllTickets");
            }
        }
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
        public AdminCinemaViewModel()
        {
            allFilms = new UnitOfWork().Film.GetAll();
            allTickets = new UnitOfWork().Ticket.GetAll(Username);
            allOrderTickets = new UnitOfWork().OrderTicket.GetAll();
        }
        private RelayCommand deleteItem = null;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    UnitOfWork unitOfWork = new UnitOfWork();
                    if (SelectedTabItem.Name == "FilmsTab" && SelectedFilm != null)
                    {
                        unitOfWork.OrderTicket.DeleteTicketsWithFilms(SelectedFilm.IdFilm);
                        unitOfWork.Ticket.Delete(SelectedFilm.IdFilm);
                        unitOfWork.Session.Delete(SelectedFilm.IdFilm);  
                        unitOfWork.Film.Delete(SelectedFilm.IdFilm);
                        resultStr = "Фильм удален и все сеансы, связанные с ним";
                        UpdateAllDataView();
                    }
                    MessageWindowCommand.ShowMessageToUser(resultStr);
                });
            }
        }

        private RelayCommand openAddFilmWindow = null;
        public RelayCommand OpenAddFilmWindow
        {
            get
            {
                return openAddFilmWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenAddFilmWindowCommand();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private RelayCommand openAddSessionWindow = null;
        public RelayCommand OpenAddSessionWindow
        {
            get
            {
                return openAddSessionWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenAddSessionWindowCommand(Username);
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private RelayCommand openAddHAllWindow = null;
        public RelayCommand OpenAddHallWindow
        {
            get
            {
                return openAddHAllWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenAddHallWindowCommand(Username);
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }

        private void OpenAddHallWindowCommand(string username)
        {
            AddHallWindow newAddHallWindow = new AddHallWindow(username);
            SetCenterPositionAndOpen(newAddHallWindow);
        }
        private void OpenAddFilmWindowCommand()
        {
            AddFilmWindow newAddFilmWindow = new AddFilmWindow();
            SetCenterPositionAndOpen(newAddFilmWindow);
        }
        private void OpenAddSessionWindowCommand(string username)
        {
            AddSessionWindow newAddSessionWindow = new AddSessionWindow(username);
            SetCenterPositionAndOpen(newAddSessionWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        #region COMMANDS Update

        private RelayCommand refreshData = null;

        public RelayCommand RefreshData
        {
            get
            {
                return refreshData ?? new RelayCommand(obj =>
                {
                    UpdateAllDataView();
                });
            }
        }
        #endregion

        #region METHODS Update
        private void UpdateAllDataView()
        {
            UpdateAllFilmsView();
            UpdateAllSessionsView();
            UpdateAllCinemasView();
        }

        private void UpdateAllFilmsView()
        {
            AllFilms = new UnitOfWork().Film.GetAll();
            AdminCinemaWindow.AllFilms.ItemsSource = null;
            AdminCinemaWindow.AllFilms.Items.Clear();
            AdminCinemaWindow.AllFilms.ItemsSource = AllFilms;
            AdminCinemaWindow.AllFilms.Items.Refresh();
        }
        private void UpdateAllSessionsView()
        {
            AllTickets = new UnitOfWork().Ticket.GetAll(Username);
            AdminCinemaWindow.AllSessions.ItemsSource = null;
            AdminCinemaWindow.AllSessions.Items.Clear();
            AdminCinemaWindow.AllSessions.ItemsSource = AllTickets;
            AdminCinemaWindow.AllSessions.Items.Refresh();
        }
        private void UpdateAllCinemasView()
        {
            AllOrderTickets = new UnitOfWork().OrderTicket.GetAll();
            AdminCinemaWindow.AllOrderTickets.ItemsSource = null;
            AdminCinemaWindow.AllOrderTickets.Items.Clear();
            AdminCinemaWindow.AllOrderTickets.ItemsSource = AllOrderTickets;
            AdminCinemaWindow.AllOrderTickets.Items.Refresh();
        }

        #endregion
    }
}
