using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class TicketsRepository : IRepository<Tickets>
    {
        private DataBaseContext db;
        public TicketsRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(Tickets item)
        {
            string result = "Сделано!\nОбновите страницу";
            db.Tickets.Add(item);
            Save();
            return result;
        }

        public void Delete(int id)
        {
            List<Tickets> tickets = db.Tickets.Include(el => el.Sessions.Film).ToList();
            List<Tickets> orderTickets = (from ticket in tickets
                                               where ticket.Sessions.IdFilm == id
                                               select ticket).ToList();
            if (orderTickets != null)
            {
                foreach (var item in orderTickets)
                {
                    db.Tickets.Remove(item);
                    Save();
                }
            }
        }

        public Tickets Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Tickets> GetAll()
        {
            return db.Tickets.ToList();
        }

        public List<Tickets> GetAll(string username)
        {
            var allTickets = db.Tickets
                              .Include(x => x.Sessions.Halls.Cinemas.AdminCinema)
                              .ToList();
            var result = (from ticket in allTickets
                          where ticket.Sessions.Halls.Cinemas.AdminCinema.Username == username
                          select ticket).ToList();
            return result;
        }

        public List<Tickets> GetSelectedAll(Film film)
        {
            var allTickets = db.Tickets
                              .Include(x => x.Sessions.Halls.Cinemas.AdminCinema)
                              .ToList();
            var result = (from ticket in allTickets
                          where ticket.Sessions.Film.IdFilm == film.IdFilm && ticket.Availability == true
                          orderby ticket.Row ascending
                          select ticket).ToList();
            return result;
        }

        public string Update(Tickets item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
