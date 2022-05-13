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
    public class UserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string URL { get; set; }

        private string searchName;
        public string SearchName
        {
            get { return searchName; }
            set
            {
                searchName = value;
                NotifyPropertyChanged("SearchName");
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged("Username");
            }
        }

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
        public UserViewModel(string currentUsername)
        {
            Username = currentUsername;
            allFilms = new UnitOfWork().Film.GetAll();
        }

        private RelayCommand sortGenre = null;
        public RelayCommand SortGenre
        {
            get
            {
                return sortGenre ?? new RelayCommand(obj =>
                {
                    try
                    {
                        String name = obj as String;
                        SortGenreMethod(name);
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }

        private RelayCommand search = null;
        public RelayCommand Search
        {
            get
            {
                return search ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    try
                    {

                        if (SearchName == null || SearchName.Replace(" ", "").Length == 0)
                        {
                            SetRedBlockControll(window, "SearchBox");
                        }
                        else
                        {
                            AllFilms = new UnitOfWork().Film.GetAll();
                            var searchFilms = from film in AllFilms
                                              where film.Name.ToLower().Contains(SearchName.ToLower())
                                              select film;
                            AllFilms = searchFilms.ToList();
                            Control block = window.FindName("SearchBox") as Control;
                            block.BorderBrush = Brushes.Gray;
                        }

                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }

        private RelayCommand getAllFilms = null;
        public RelayCommand GetAllFilms
        {
            get
            {
                return getAllFilms ?? new RelayCommand(obj =>
                {
                    try
                    {
                        AllFilms = new UnitOfWork().Film.GetAll();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }

        private RelayCommand openUserInfWindow = null;
        public RelayCommand OpenUserInfWindow
        {
            get
            {
                return openUserInfWindow ?? new RelayCommand(obj =>
                {
                    try
                    {
                        OpenUserInfWindowCommand();
                    }
                    catch (Exception e)
                    {
                        MessageWindowCommand.ShowMessageToUser(e.Message);
                    }
                });
            }
        }
        private void OpenUserInfWindowCommand()
        {
            UserInformationWindow newUserInfWindow = new UserInformationWindow();
            SetCenterPositionAndOpen(newUserInfWindow);
        }
        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
        private void SearchMethod(string name)
        {
            AllFilms = new UnitOfWork().Film.GetAll();
            var searchFilm = from film in AllFilms
                             where film.Name.Contains(name)
                             orderby film.Name
                             select film;
            AllFilms = searchFilm.ToList();
        }
        private void SortGenreMethod(string genre)
        {
            AllFilms = new UnitOfWork().Film.GetAll();
            var sortFilms = from film in AllFilms
                            where film.Genre.Contains(genre)
                            orderby film.Name
                            select film;
            AllFilms = sortFilms.ToList();
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
