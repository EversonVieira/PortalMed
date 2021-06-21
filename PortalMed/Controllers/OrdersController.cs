using Core.Enums;
using Core.Models;
using Domain.Models;
using Domain.Models.Filters;
using Domain.Rules;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PortalMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController
    {
        private OrderRule _Rule;

        public OrdersController(OrderRule orderRule)
        {
            this._Rule = orderRule;
        }

        [HttpPost()]
        public async Task<Response<bool>> Save(Order order)
        {
            try
            {
                return await this._Rule.Insert(order);
            }
            catch
            {
                return new Response<bool>
                {
                    Data = false,
                    Errors = null,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
        [HttpGet("{Id},{userId},{startDate},{endDate},{sortMethod}")]
        public async Task<Response<List<Order>>> GetAll(int Id = 0,
                                                       int userId = 0,
                                                       DateTime startDate = new DateTime(),
                                                       DateTime endDate = new DateTime(),
                                                       SortMethodEnum sortMethod = 0)
        {

            OrderFilters filter = null;

            if (!(userId > 0) || !(Id > 0) ||
               !startDate.Equals(DateTime.MinValue) ||
               !endDate.Equals(DateTime.MinValue) ||
               sortMethod == SortMethodEnum.DESC
            )
            {
                filter = new OrderFilters
                {
                    Id = Id,
                    UserId = userId,
                    StartDate = startDate,
                    EndDate = endDate,
                    SortMethod = sortMethod
                };
            }

            return await this._Rule.GetAll(filter);
        }
    }
}
