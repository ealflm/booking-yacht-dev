﻿using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookingYacht.Business.NotificationUtils;
using FirebaseAdmin.Messaging;

namespace BookingYacht.API.Controllers.Agency
{
    [Route(AgencyRoute)]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = Role)]
    public class OrdersController : BaseAgencyController
    {
        private readonly IOrdersService _service;
        private const string PrivateKey = "AppData/Firebase/firebase-admin.json";

        public OrdersController(IOrdersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] OrdersSearchModel model)
        {
            var order = await _service.Search(model);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _service.GetNavigation(id);
            return order != null ? Success(order) : Fail("The Order's not exist");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreateModel model)
        {
            var res = await _service.Add(model);
            var end = DateTime.Now;
            return Success(res.Id);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] OrdersViewModel model)
        {
            var update = await _service.Update(id, model);
            return update ? Success() : Fail("The Order's not exist");
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var delete = await _service.Delete(id);
            return delete ? Success() : Fail("The Order's not exist");
        }
    }
}