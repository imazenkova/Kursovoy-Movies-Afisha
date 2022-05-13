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

namespace MoviesAfisha.ViewModels.UserViewModels
{
    public class OrderTicketsViewModel : BaseViewModel
    {
        public Sessions Time { get; set; }
        public Sessions Date { get; set; }
        public Cinemas Cinema { get; set; }
        public static Film SelectedFilm { get; set; }
        public static string Username { get; set; }
        public Tickets SelectedTicket { get; set; }

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

        private List<Cinemas> allCinemas;
        public List<Cinemas> AllCinemas
        {
            get { return allCinemas; }
            set
            {
                allCinemas = value;
                NotifyPropertyChanged("SelectedTickets");
            }
        }

        private List<Sessions> allSessions;
        public List<Sessions> AllSessions
        {
            get { return allSessions; }
            set
            {
                allSessions = value;
                NotifyPropertyChanged("SelectedTickets");
            }
        }

        private List<Tickets> selectedTickets; 
        public List<Tickets> SelectedTickets
        {
            get { return selectedTickets; }
            set
            {
                selectedTickets = value;
                NotifyPropertyChanged("SelectedTickets");
            }
        }

        public OrderTicketsViewModel()
        {
            allCinemas = new UnitOfWork().Cinemas.GetAll(); 
            allFilms = new UnitOfWork().Film.GetAll();
            allSessions = new UnitOfWork().Session.GetAll(SelectedFilm);
            selectedTickets =  new UnitOfWork().Ticket.GetSelectedAll(SelectedFilm);
        }

        private RelayCommand sortTickets = null;
        public RelayCommand SortTickets
        {
            get
            {
                return sortTickets ?? new RelayCommand(obj =>
                {
                    try
                    {
                        SelectedTickets = new UnitOfWork().Ticket.GetSelectedAll(SelectedFilm);
                        if (Date == null || Time == null || Cinema == null)
                        {
                            MessageWindowCommand.ShowMessageToUser("Заполните все поля поиска");
                        }
                        else
                        {
                            SelectedTickets = (from ticket in SelectedTickets
                                               where ticket.Sessions.Halls.Cinemas.Name == Cinema.Name && ticket.Sessions.Date == Date.Date && ticket.Sessions.Time == Time.Time
                                               orderby ticket.Row
                                               select ticket).ToList();
                            if (SelectedTickets.Count == 0)
                            {
                                MessageWindowCommand.ShowMessageToUser("Билетов нет");
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private RelayCommand refreshTickets = null;
        public RelayCommand RefreshTickets
        {
            get
            {
                return refreshTickets ?? new RelayCommand(obj =>
                {

                    try
                    {
                        SelectedTickets = new UnitOfWork().Ticket.GetSelectedAll(SelectedFilm);
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private RelayCommand addOrderTicketWindow = null;
        public RelayCommand AddOrderTicketWindow
        {
            get
            {
                return addOrderTicketWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        if (SelectedTicket == null)
                        {
                            MessageWindowCommand.ShowMessageToUser("Выберите билет");
                        }
                        else
                        {
                            OpenAddOrderTicketWindowCommand(SelectedTicket, Username);
                        }
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private void OpenAddOrderTicketWindowCommand(Tickets ticket, string username)
        {
            AddOrderTicketWindow newAddOrderTicketWindow = new AddOrderTicketWindow(ticket, username);
            SetCenterPositionAndOpen(newAddOrderTicketWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}
