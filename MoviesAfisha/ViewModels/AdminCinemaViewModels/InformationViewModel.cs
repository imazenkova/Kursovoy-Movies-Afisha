using MoviesAfisha.Models.DataBaseModels;
using MoviesAfisha.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.ViewModels.AdminCinemaViewModels
{
    public class InformationViewModel :BaseViewModel
    {
        private List<Cinemas> allCinemas;
        public List<Cinemas> AllCinemas
        {
            get { return allCinemas; }
            set
            {
                allCinemas = value;
                NotifyPropertyChanged("AllAdminCinemas");
            }
        }
        public InformationViewModel()
        {
            allCinemas = new UnitOfWork().Cinemas.GetAll();
        }
    }
}
