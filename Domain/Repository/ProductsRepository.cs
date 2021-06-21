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
    internal class ProductsRepository
    {
        private AppDbContext appDbContext;
        public ProductsRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Response<bool>> Insert(Product product)
        {
            try
            {
                var response = await appDbContext.Products.AddAsync(product);

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

        public async Task<Response<bool>> Update(Product product)
        {
            try
            {
                appDbContext.Entry(product).State = EntityState.Modified;

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

        public async Task<Response<List<Product>>> GetAll(ProductFilter filter)
        {
            try
            {
                Response<List<Product>> response = null;

                await Task.Run(() =>
                {
                    List<Product> returnValues;
                    if (filter is not null)
                    {
                        returnValues = filter.SortMethod == SortMethodEnum.ASC ?
                                                    appDbContext.Products.Where(GetFilterPredicate(filter)).ToList() :
                                                    appDbContext.Products.Where(GetFilterPredicate(filter)).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        returnValues = appDbContext.Products.Where(GetFilterPredicate(filter)).ToList();
                    }
                    response = new Response<List<Product>>
                    {
                        Data = returnValues,
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Errors = null
                    };
                });

                return response;
            }
            catch(Exception ex)
            {
                return new Response<List<Product>>
                {
                    Data = null,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
          
        }

        private Func<Product, bool> GetFilterPredicate(ProductFilter filter)
        {
            if (filter is null)
            {
                return x => true;
            }
            Func<Product, bool> predicate = x => (string.IsNullOrWhiteSpace(filter.Name) || x.Name.ToLower().Contains(filter.Name.ToLower())) &&
                        (string.IsNullOrWhiteSpace(filter.Description) || x.Description.ToLower().Contains(filter.Description.ToLower())) &&
                        ((filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue) || (x.CreationDate >= filter.StartDate && x.CreationDate <= filter.EndDate));
            return predicate;
        }
    }
}
