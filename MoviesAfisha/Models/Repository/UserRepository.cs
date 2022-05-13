using MoviesAfisha.Models.DataBaseModels;
using System.Collections.Generic;
using System.Linq;

namespace MoviesAfisha.Models.Repository
{
    public class UserRepository:IRepository<User>
    {
        private DataBaseContext db;

        public UserRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(User item)
        {
            string result = "Такой пользователь уже существует";
            bool checkIsExist = db.User.Any(el => el.Username == item.Username);
            if (!checkIsExist)
            {
                db.User.Add(item);
                Save();
                result = "Сделано!\nОбновите страницу";
            }
            return result;
        }
        public string RegisterUser(User item)
        {
            string result = "Это имя пользователя\n уже занято";
            bool checkIsExist = db.User.Any(el => el.Username == item.Username);
            if (!checkIsExist)
            {
                db.User.Add(item);
                Save();
                result = "Сделано!\nТеперь вы можете зайти в аккаунт.";
            }
            return result;
        }

        public void Delete(int id)
        {
            User user = db.User.Find(id);
            if (user != null)
            {
                db.User.Remove(user);
                Save();
            }
        }

        public User Get(int id)
        {
            return db.User.Find(id);
        }

        public List<User> GetAll()
        {
            return db.User.ToList();
        }

        public string Update(User item)
        {
            string result = "Такого пользователя не существует";
            User user = db.User.FirstOrDefault(el => el.Username == item.Username);
            if (user != null)
            {
                user.Username = item.Username;
                user.Password = item.Password;
                Save();
                result = "Сделано! \nПользователю " + user.Username + " задан новый пароль";
            }
            return result;
        }
        public string UpdateUsername(User item,string username)
        {
            string result = "Что-то пошло не так";
            User user = db.User.FirstOrDefault(el => el.Username == username);
            if (user != null)
            {
                user.Username = item.Username;
                user.Password = item.Password;
                Save();
                result = "Сделано! \nВаш новый ник " + user.Username + "\nПерезайдите в профиль под новым ником";
            }
            return result;
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
