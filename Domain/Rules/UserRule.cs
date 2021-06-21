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
    public class UserRule
    {
        private UsersRepository repository;
        public UserRule(AppDbContext appDbContext)
        {
            this.repository = new UsersRepository(appDbContext);
        }

        public async Task<Response<bool>> Save(User user)
        {
            if (user.ThreatAndValidate())
            {
                return await repository.Save(user);
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
        public async Task<Response<List<User>>> GetAll(UserFilter filter)
        {
            var response =  await repository.GetAll(filter);

            response.Data.ForEach(user =>
            {
                user.Password = string.Empty;
            });

            return response;

        }
    }
}
