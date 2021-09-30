﻿using System;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface ITicketTypeService
    {
        Task<List<TicketTypeViewModel>> SearchTicketTypesForAdmin(TicketTypeSearchModel model = null);
        Task SetStatusTicketType(Guid id, TicketTypeViewModel model);
    }
}
