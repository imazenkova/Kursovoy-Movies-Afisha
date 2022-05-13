namespace MoviesAfisha.Models.DataBaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderTickets
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        public int IdTicket { get; set; }

        public virtual Tickets Tickets { get; set; }

        public virtual User User { get; set; }
    }
}
