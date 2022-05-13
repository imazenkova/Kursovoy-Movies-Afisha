using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class SessionsRepository : IRepository<Sessions>
    {
        private DataBaseContext db;
        public SessionsRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(Sessions item)
        {
            string result = "Время занято.\nВыберите другое.";
            bool checkIsExist = db.Sessions.Any(el => el.Time == item.Time && el.Date == item.Date);
            if (!checkIsExist)
            {
                db.Sessions.Add(item);
                Save();
                result = "Сделано!\nОбновитдуе страницу";
            }
            return result;
        }

        public void Delete(int id)
        {
            List<Sessions> sessions = db.Sessions.ToList();
            List<Sessions> ticketsSession = (from session in sessions
                                               where session.IdFilm == id
                                               select session).ToList();
            if (ticketsSession != null)
            {
                foreach (var item in ticketsSession)
                {
                    db.Sessions.Remove(item);
                    Save();
                }
            }
        }

        public Sessions Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sessions> GetAll()
        {
            return db.Sessions.ToList();
        }
        public List<Sessions> GetAll(Film film)
        {
            var allSessions = db.Sessions
                              .Include(x => x.Film)
                              .ToList();
            var result = (from session in allSessions
                          where session.IdFilm == film.IdFilm
                          select session).ToList();
            return result;
        }

        public string Update(Sessions item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
