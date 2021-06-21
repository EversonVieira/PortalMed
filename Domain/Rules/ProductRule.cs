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
    public class ProductRule
    {
        private ProductsRepository repository;

        public ProductRule(AppDbContext dbContext)
        {
            this.repository = new ProductsRepository(dbContext);
        }

        public async Task<Response<bool>> Insert(Product product)
        {
            if (product.ThreatAndValidate())
            {
                return await repository.Insert(product);
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
        public async Task<Response<bool>> Update(Product product)
        {
            if (product.ThreatAndValidate() && product.Id > 0)
            {
                return await repository.Update(product);
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
        public async Task<Response<List<Product>>> GetAll(ProductFilter filter)
        {
            var response = await repository.GetAll(filter);

            return response;

        }


    }
}
