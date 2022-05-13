using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class OrderTicketsRepository : IRepository<OrderTickets>
    {
        private DataBaseContext db;
        public OrderTicketsRepository(DataBaseContext context)
        {
            db = context;
        }
        public string Create(OrderTickets item)
        {
            string result = "Что-то пошло не так";
            db.OrderTickets.Add(item);
            Save();
            return result;
        }

        public void Delete(int id)
        {
            List<OrderTickets> orderTickets = db.OrderTickets.ToList();
            orderTickets = (from currentTicket in orderTickets
                            where currentTicket.IdUser == id
                            select currentTicket).ToList();
            Tickets ticket = null;
            if (orderTickets != null)
            {
                foreach (var item in orderTickets)
                {
                    ticket = db.Tickets.FirstOrDefault(el => el.Id == item.IdTicket);
                    if (ticket != null)
                    {
                        ticket.Availability = true;
                        Save();
                    }
                    db.OrderTickets.Remove(item);
                    Save();
                }
            }
        }
        public void DeleteTicketsWithFilms(int id)
        {
            List<OrderTickets> tickets = db.OrderTickets.Include(el => el.Tickets.Sessions.Film).ToList();
            List<OrderTickets> orderTickets = (from ticket in tickets
                                               where ticket.Tickets.Sessions.IdFilm == id
                                               select ticket).ToList();
            if (orderTickets != null)
            {
                foreach (var item in orderTickets)
                {
                    db.OrderTickets.Remove(item);
                    Save();
                }
            }
        }

        public OrderTickets Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<OrderTickets> GetAll()
        {
            var result = db.OrderTickets
                             .Include(x => x.User)
                             .ToList();
            return result;
        }
        public List<OrderTickets> GetUserOrderTickets(string username)
        {
            var idUser = db.User.ToList();
            User user = null;

            foreach (User item in idUser)
            {
                if (username == item.Username)
                    user = item;
            }
            var allOrderTickets = db.OrderTickets
                           .Include(x => x.Tickets.Sessions.Film)
                           .ToList();
            var result = (from ticket in allOrderTickets
                          where ticket.IdUser == user.Id
                          select ticket).ToList();
            return result;
        }

        public string Update(OrderTickets item)
        {
            string str = "Что-то пошло не так";
            List<OrderTickets> orderTickets = db.OrderTickets.ToList();
            orderTickets = (from currentTicket in orderTickets
                            where currentTicket.Id == item.Id
                            select currentTicket).ToList();

            if (orderTickets != null)
            {
                foreach (var currentOrderTicket in orderTickets)
                {
                    db.OrderTickets.Remove(currentOrderTicket);
                    Save();
                }

                var allTickets = db.Tickets.ToList();
                Tickets ticket = db.Tickets.FirstOrDefault(el => el.Id == item.IdTicket);
                if (ticket != null)
                {
                    ticket.Availability = true;
                    Save();
                }

              
                str = "Билет отменен";
            }
            return str;
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
