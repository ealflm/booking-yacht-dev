using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.InsertModels
{
    public class TicketsInsertModel
    {
        public Guid IdOrder { get; set; }
        public Guid IdTrip { get; set; }
        public List<string> CustomerNames { get; set; }
        public List<string> Phones { get; set; }
        public List<Guid> IdTicketTypes { get; set; }
    }
}