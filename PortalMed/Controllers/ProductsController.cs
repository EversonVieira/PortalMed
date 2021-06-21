using Core.Enums;
using Core.Models;
using Domain.Models;
using Domain.Models.Filters;
using Domain.Rules;
using Microsoft.AspNetCore.Http;
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
    public class ProductsController : ControllerBase
    {
        private ProductRule _Rule;

        public ProductsController(ProductRule rule)
        {
            this._Rule = rule;
        }

        [HttpPost()]
        public async Task<Response<bool>> Insert(Product product)
        {
            try
            {
                return await this._Rule.Insert(product);
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
        [HttpPut("{id}")]
        public async Task<Response<bool>> Update(int id,Product product)
        {
            try
            {
                if (product.Id != id)
                    product.Id = id;

                return await this._Rule.Update(product);
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
        [HttpGet("{name},{description},{startDate},{endDate},{sortMethod}")]
        public async Task<Response<List<Product>>> GetAll(string name = "",
                                                       string description = "",
                                                       DateTime startDate = new DateTime(),
                                                       DateTime endDate = new DateTime(),
                                                       SortMethodEnum sortMethod = 0)
        {

            ProductFilter filter = null;

            if (!string.IsNullOrWhiteSpace(name) ||
               !string.IsNullOrWhiteSpace(description) ||
               !startDate.Equals(DateTime.MinValue) ||
               !endDate.Equals(DateTime.MinValue) ||
               sortMethod == SortMethodEnum.DESC
            )
            {
                filter = new ProductFilter
                {
                    Name = name,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    SortMethod = sortMethod
                };
            }

            return await this._Rule.GetAll(filter);
        }
    }
}
