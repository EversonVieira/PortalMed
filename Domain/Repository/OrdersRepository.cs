using Core.Enums;
using Core.Models;
using Domain.Models;
using Domain.Models.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal class OrdersRepository
    {
        private AppDbContext appDbContext;
        public OrdersRepository(AppDbContext dbContext)
        {
            this.appDbContext = dbContext;
        }

        public async Task<Response<bool>> Insert(Order order)
        {
            try
            {
                var response = await appDbContext.Orders.AddAsync(order);

                appDbContext.Entry(order).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();

                await Task.CompletedTask;

                return new Response<bool>
                {
                    Data = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
            catch
            {
                return new Response<bool>
                {
                    Data = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
          
        }

        public async Task<Response<List<Order>>> GetAll(OrderFilters filter)
        {
            try
            {
                Response<List<Order>> response = null;

                await Task.Run(() =>
                {
                    List<Order> returnValues;
                    if (filter is not null)
                    {
                        returnValues = filter.SortMethod == SortMethodEnum.ASC ?
                                                    appDbContext.Orders.Where(GetFilterPredicate(filter)).ToList() :
                                                    appDbContext.Orders.Where(GetFilterPredicate(filter)).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        returnValues = appDbContext.Orders.Where(GetFilterPredicate(filter)).ToList();
                    }
                    response = new Response<List<Order>>
                    {
                        Data = returnValues,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Errors = null
                    };
                });

                return response;
            }
            catch
            {
                return new Response<List<Order>>
                {
                    Data = null,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
           
        }

        private Func<Order, bool> GetFilterPredicate(OrderFilters filter)
        {
            if (filter is null)
            {
                return x => true;
            }
            Func<Order, bool> predicate = x => (filter.Id == 0 || x.Id.Equals(filter.Id)) &&
                        ((filter.UserId == 0 || x.UserId.Equals(filter.UserId)) &&
                        ((filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue) || (x.PurchaseDate >= filter.StartDate && x.PurchaseDate <= filter.EndDate)));
            return predicate;
        }
    }
}
