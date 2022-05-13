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
    public class AddFilmViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Time { get; set; }
        public string URL { get; set; }
        private RelayCommand addFilm = null;
        public RelayCommand AddFilm
        {
            get
            {
                return addFilm ?? new RelayCommand(obj =>
                {

                    Window window = obj as Window;
                    int time = Convert.ToInt32(Time);
                  
                    string resultStr = "";

                    if (Name == null || Name.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "NameBox");
                    }
                    else if (Country == null || Country.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "CountryBox");
                    }
                    else if (Director == null || Director.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "DirectorBox");
                    }
                    else if (Genre == null || Genre.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(window, "GenreBox");
                    }
                    else if (time == 0)
                    {
                        SetRedBlockControll(window, "TimeBox");
                    }
                    else
                    {
                        if (URL == null || URL.Replace(" ", "").Length == 0)
                        {
                            URL = "C:\\Documents\\Cinema_Kursovoy\\MoviesAfisha\\Images\\MoviesPosters\\error.jpg";
                        }
                        else
                        {
                            URL = "C:\\Documents\\Cinema_Kursovoy\\MoviesAfisha\\Images\\MoviesPosters\\" + URL;
                        }

                        Film newFilm = new Film
                        {
                            Name = this.Name,
                            Country = this.Country,
                            Director = this.Director,
                            Genre = this.Genre,
                            Time = time,
                            URL = this.URL
                        };

                        UnitOfWork unitOfWork = new UnitOfWork();
                        resultStr = unitOfWork.Film.Create(newFilm);

                        MessageWindowCommand.ShowMessageToUser(resultStr);
                        ClearTextBox();
                        window.Close();
                    }

                });
            }
        }
        private void ClearTextBox()
        {
            Name = null;
            Country = null;
            Director = null;
            Genre = null;
            Time = null;
            URL = null;
        }
        private void SetRedBlockControll(Window window, string blockName)
        {
            Control block = window.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }
    }
}
