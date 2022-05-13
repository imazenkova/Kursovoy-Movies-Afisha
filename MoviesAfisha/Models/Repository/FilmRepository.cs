using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class FilmRepository : IRepository<Film>
    {
        private DataBaseContext db;
        public FilmRepository(DataBaseContext context)
        {
            db = context;
        }

        public string Create(Film item)
        {
            string result = "Уже существует.\nДобавьте новый фильм";
            bool checkIsExist = db.Film.Any(el => el.Name == item.Name && el.URL == item.URL);
            if (!checkIsExist)
            {
                db.Film.Add(item);
                Save();
                result = "Сделано!\nОбновите страницу";
            }
            return result;
        }

        public void Delete(int id)
        {
            Film films = db.Film.Find(id);
            if (films != null)
            {
                db.Film.Remove(films);
                Save();
            }
        }

        public Film Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Film> GetAll()
        {
            return db.Film.ToList();
        }
        public List<Film> Search(string name)
        {
            return db.Film.Where(el=>el.Name.Contains(name)).ToList();
        }
        public string Update(Film item)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
