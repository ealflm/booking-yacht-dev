﻿using System;
using System.Threading.Tasks;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingYacht.API.Controllers.Admin
{
    [Route(ApiVer1Route)]
    [ApiController]
    [Authorize]
    public class OrdersController: BaseApiVer1Controller
    {
        private readonly IOrdersService _service;

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] OrdersSearchModel model)
        {
            var order = await _service.SearchNavigation(model);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _service.GetNavigation(id);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }
        
    }
}