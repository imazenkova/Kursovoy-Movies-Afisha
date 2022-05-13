using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace MoviesAfisha.Models.DataBaseModels
{
    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DataBaseContext2")
        {
        }

        public virtual DbSet<AdminCinema> AdminCinema { get; set; }
        public virtual DbSet<Cinemas> Cinemas { get; set; }
        public virtual DbSet<Film> Film { get; set; }
        public virtual DbSet<Halls> Halls { get; set; }
        public virtual DbSet<OrderTickets> OrderTickets { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<Tickets> Tickets { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminCinema>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<AdminCinema>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<AdminCinema>()
                .HasMany(e => e.Cinemas)
                .WithRequired(e => e.AdminCinema)
                .HasForeignKey(e => e.IdAdmin)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cinemas>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Cinemas>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Cinemas>()
                .HasMany(e => e.Halls)
                .WithRequired(e => e.Cinemas)
                .HasForeignKey(e => e.IdCinema)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Director)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Genre)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Halls>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Halls)
                .HasForeignKey(e => e.IdHall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderTickets>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<OrderTickets>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<Sessions>()
                .Property(e => e.Date)
                .IsUnicode(false);

            modelBuilder.Entity<Sessions>()
                .Property(e => e.Time)
                .IsUnicode(false);

            modelBuilder.Entity<Sessions>()
                .HasMany(e => e.Tickets)
                .WithRequired(e => e.Sessions)
                .HasForeignKey(e => e.IDSession)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tickets>()
                .HasMany(e => e.OrderTickets)
                .WithRequired(e => e.Tickets)
                .HasForeignKey(e => e.IdTicket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.OrderTickets)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.IdUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }

    interface IRepository<T> where T : class
    {
        List<T> GetAll();
        T Get(int id);
        string Create(T item);
        string Update(T item);
        void Delete(int id);
    }
}
