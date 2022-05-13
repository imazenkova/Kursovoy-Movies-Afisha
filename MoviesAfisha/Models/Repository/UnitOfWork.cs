using MoviesAfisha.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAfisha.Models.Repository
{
    public class UnitOfWork
    {
        private DataBaseContext db = new DataBaseContext();
        private AdminCinemaRepository AdminCinemaRepository;
        private UserRepository UserRepository;
        private CinemasRepository CinemasRepository;
        private HallsRepository HallsRepository;
        private FilmRepository FilmRepository;
        private SessionsRepository SessionsRepository;
        private TicketsRepository TicketsRepository;
        private OrderTicketsRepository OrderTicketsRepository;
        public AdminCinemaRepository AdminCinema
        {
            get
            {
                if(AdminCinemaRepository == null)
                {
                    AdminCinemaRepository = new AdminCinemaRepository(db);
                }
                return AdminCinemaRepository;
            }
        }
        public UserRepository User
        {
            get
            {
                if (UserRepository == null)
                {
                    UserRepository = new UserRepository(db);
                }
                return UserRepository;
            }
        }
        public CinemasRepository Cinemas
        {
            get
            {
                if (CinemasRepository == null)
                {
                    CinemasRepository = new CinemasRepository(db);
                }
                return CinemasRepository;
            }
        }
        public HallsRepository Halls
        {
            get
            {
                if (HallsRepository == null)
                {
                    HallsRepository = new HallsRepository(db);
                }
                return HallsRepository;
            }
        }
        public FilmRepository Film
        {
            get
            {
                if (FilmRepository == null)
                {
                    FilmRepository = new FilmRepository(db);
                }
                return FilmRepository;
            }
        }
        public SessionsRepository Session
        {
            get
            {
                if (SessionsRepository == null)
                {
                    SessionsRepository = new SessionsRepository(db);
                }
                return SessionsRepository;
            }
        }

        public TicketsRepository Ticket
        {
            get
            {
                if (TicketsRepository == null)
                {
                    TicketsRepository = new TicketsRepository(db);
                }
                return TicketsRepository;
            }
        }
        public OrderTicketsRepository OrderTicket
        {
            get
            {
                if (OrderTicketsRepository == null)
                {
                    OrderTicketsRepository = new OrderTicketsRepository(db);
                }
                return OrderTicketsRepository;
            }
        }
    }
}
