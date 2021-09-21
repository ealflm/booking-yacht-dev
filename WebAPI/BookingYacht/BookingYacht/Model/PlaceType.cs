using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BookingYacht.Model
{
    public partial class PlaceType
    {
        public PlaceType()
        {
            Destinations = new HashSet<Destination>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Destination> Destinations { get; set; }
    }
}
