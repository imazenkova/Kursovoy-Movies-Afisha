namespace MoviesAfisha.Models.DataBaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(150)]
        public string Password { get; set; }
    }
}
