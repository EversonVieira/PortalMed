using Core.Models;
using Domain.Models;
using Domain.Models.Filters;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rules
{
    public class OrderRule
    {
        private OrdersRepository repository;

        public OrderRule(AppDbContext dbContext)
        {
            this.repository = new OrdersRepository(dbContext);
        }

        public async Task<Response<bool>> Insert(Order order)
        {
            if (order.IsValid())
            {
                return await repository.Insert(order);
            }
            else
            {
                return new Response<bool>
                {
                    Data = false,
                    Errors = null,
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
        }
       
        public async Task<Response<List<Order>>> GetAll(OrderFilters filter)
        {
            var response = await repository.GetAll(filter);

            return response;

        }
    }
}
