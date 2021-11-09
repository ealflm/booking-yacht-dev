using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.InsertModels
{
    public class DestinationTourInsertModel
    {
        public Guid IdTour { get; set; }
        public List<Guid> IdDestinationList { get; set; }
    }
}