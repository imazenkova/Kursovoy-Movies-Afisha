using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class HallsRepository:IRepository<Halls>
    {
        private DataBaseContext db;
        public HallsRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(Halls item)
        {
            string result = "Это название уже существует";
            bool checkIsExist = db.Halls.Any(el =>item.IdCinema == el.IdCinema && el.Name == item.Name);
            if (!checkIsExist)
            {
                db.Halls.Add(item);
                Save();
                result = "Сделано!\nНовый зал добавлен";
            }
            return result;
        }

        public void Delete(int id)
        {
            Halls hall = db.Halls.Find(id);
            if (hall != null)
            {
                db.Halls.Remove(hall);
            }
        }

        public Halls Get(int id)
        {
            return db.Halls.Find(id);
        }

        //get halls current cinema
        public List<Halls> GetAll(string username)
        {
            var allHalls = db.Halls
                              .Include(x => x.Cinemas.AdminCinema)
                              .ToList();
            var result = (from hall in allHalls
                          where hall.Cinemas.AdminCinema.Username == username
                          select hall).ToList();

            return result;
        }

        public List<Halls> GetAll()
        {
            throw new NotImplementedException();
        }

        public string Update(Halls item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
