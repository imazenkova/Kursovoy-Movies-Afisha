using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class CinemasRepository:IRepository<Cinemas>
    {
        private DataBaseContext db;
        public CinemasRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(Cinemas item)
        {
            string result = "";
            bool checkIsExist = db.Cinemas.Any(el => el.Name == item.Name);
            if (!checkIsExist)
            {
                db.Cinemas.Add(item);
                Save();
                result = "Сделано!\nОбновите страницу";
            }
            return result;
        }

        public void Delete(int id)
        {
            Cinemas cinemas = db.Cinemas.Find(id);
            if (cinemas != null)
            {
                db.Cinemas.Remove(cinemas);
            }
        }

        public Cinemas Get(int id)
        {
            return db.Cinemas.Find(id);
        }

        public List<Cinemas> GetAll()
        {
            return db.Cinemas.Include(el => el.AdminCinema).ToList();
        }

        public string Update(Cinemas item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
