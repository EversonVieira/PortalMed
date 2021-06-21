using Core.Enums;
using Core.Models;
using Domain.Models;
using Domain.Models.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    internal class UsersRepository
    {
        private AppDbContext appDbContext;

        public UsersRepository(AppDbContext dbContext)
        {
            this.appDbContext = dbContext;
        }

        public async Task<Response<bool>> Save(User user)
        {
            try
            {
                if (user.Id is 0)
                {
                    var response = await appDbContext.Users.AddAsync(user);
                }
                else
                {
                    appDbContext.Entry(user).State = EntityState.Modified;
                }

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

        public async Task<Response<User>> Select(UserLogin userLogin)
        {
            return new Response<User>
            {
                Data = await appDbContext.Users.FindAsync(new User { UserName = userLogin.UserName, Password = userLogin.Password }),
                StatusCode = System.Net.HttpStatusCode.OK,
                Errors = null
            };
        }
        public async Task<Response<List<User>>> GetAll(UserFilter filter)
        {
            try
            {
                Response<List<User>> response = null;

                await Task.Run(() =>
                {
                    List<User> returnValues;
                    if (filter is not null)
                    {
                        returnValues = filter.SortMethod == SortMethodEnum.ASC ?
                                                    appDbContext.Users.Where(GetFilterPredicate(filter)).ToList() :
                                                    appDbContext.Users.Where(GetFilterPredicate(filter)).OrderByDescending(x => x.Id).ToList();
                    }
                    else
                    {
                        returnValues = appDbContext.Users.Where(GetFilterPredicate(filter)).ToList();
                    }
                    response = new Response<List<User>>
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
                return new Response<List<User>>
                {
                    Data = null,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Errors = null
                };
            }
           
        }

        private Func<User, bool> GetFilterPredicate(UserFilter filter)
        {
            if (filter is null)
            {
                return x => true;
            }
             Func<User,bool> predicate = x => (string.IsNullOrWhiteSpace(filter.DisplayName) || x.DisplayName.ToLower().Contains(filter.DisplayName.ToLower())) &&
                        (string.IsNullOrWhiteSpace(filter.UserName) || x.UserName.ToLower().Equals(filter.UserName.ToLower())) &&
                        (string.IsNullOrWhiteSpace(filter.Email) || x.Email.ToLower().Equals(filter.Email.ToLower())) &&
                        ((filter.StartDate == DateTime.MinValue || filter.EndDate == DateTime.MinValue) || (x.RegisterDate >= filter.StartDate && x.RegisterDate <= filter.EndDate));
            return predicate;
        }

    }
}
