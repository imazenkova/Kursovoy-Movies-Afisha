using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class AdminCinemaRepository : IRepository<AdminCinema>
    {
        private DataBaseContext db;

        public AdminCinemaRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(AdminCinema item)//добавление при регистрации + не повтор имени
        {
            string result = "";
            bool checkIsExist = db.AdminCinema.Any(el => el.Username == item.Username);
            if (!checkIsExist)
            {
                db.AdminCinema.Add(item);
                Save();
                result = "Сделано!\nОбновите страницу";
            }
            return result;
        }

        public void Delete(int id)
        {
            AdminCinema adminCinema = db.AdminCinema.Find(id);
            if (adminCinema != null)
            {
                db.AdminCinema.Remove(adminCinema);
            }
        }

        public AdminCinema Get(int id)
        {
            return db.AdminCinema.Find(id);
        }

        public List<AdminCinema> GetAll()
        {
            return db.AdminCinema.ToList();
        }

        public string Update(AdminCinema item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
