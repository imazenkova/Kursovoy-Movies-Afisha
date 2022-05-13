namespace MoviesAfisha.Models.DataBaseModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tickets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tickets()
        {
            OrderTickets = new HashSet<OrderTickets>();
        }

        public int Id { get; set; }

        public int IDSession { get; set; }

        public int Row { get; set; }

        public int Place { get; set; }

        public int Price { get; set; }

        public bool Availability { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderTickets> OrderTickets { get; set; }

        public virtual Sessions Sessions { get; set; }
    }
}
