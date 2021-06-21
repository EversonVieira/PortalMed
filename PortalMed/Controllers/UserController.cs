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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PortalMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserRule _Rule;

        public UserController(UserRule _Rule)
        {
            this._Rule = _Rule;
        }
        [HttpPost("signup")]
        public async Task<Response<bool>> Save(User user)
        {
            try
            {
                return await this._Rule.Save(user);
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

        [HttpGet("users/{userName},{displayName},{startDate},{endDate},{email},{sortMethod}")]
        public async Task<Response<List<User>>> GetAll(string userName = "",
                                                       string displayName = "",
                                                       DateTime startDate = new DateTime(),
                                                       DateTime endDate = new DateTime(),
                                                       string email = "",
                                                       SortMethodEnum sortMethod = 0)
        {

            UserFilter filter = null;

            if (!string.IsNullOrWhiteSpace(userName) ||
               !string.IsNullOrWhiteSpace(displayName) ||
               !string.IsNullOrWhiteSpace(email) ||
               !startDate.Equals(DateTime.MinValue) ||
               !endDate.Equals(DateTime.MinValue) ||
               sortMethod == SortMethodEnum.DESC
            )
            {
                filter = new UserFilter
                {
                    UserName = userName,
                    DisplayName = displayName,
                    StartDate = startDate,
                    EndDate = endDate,
                    Email = email,
                    SortMethod = sortMethod
                };
            }

            return await this._Rule.GetAll(filter);
        }
    }
}
