using Domain.Interfaces;
using Entities.Entities;
using Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryUser : RepositoryGenerics<ApplicationUser>, IUser
    {

        private readonly DbContextOptions<Context> _optionsbuilder;

        public RepositoryUser()
        {
            _optionsbuilder = new DbContextOptions<Context>();
        }

        public async Task<bool> AddUser(string email, string password, int age, string phone)
        {
            try
            {
                using (var data = new Context(_optionsbuilder))
                {
                    await data.ApplicationUser.AddAsync(
                          new ApplicationUser
                          {
                              Email = email,
                              PasswordHash = password,
                              Age = age,
                              Phone = phone,
                              Type = UserType.CommonUser
                          });

                    await data.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckUser(string email, string password)
        {
            try
            {
                using (var data = new Context(_optionsbuilder))
                {
                    return await data.ApplicationUser.
                           Where(x => x.Email.Equals(email) && 
                           x.PasswordHash.Equals(password))
                           .AsNoTracking()
                           .AnyAsync();
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> ReturnIdUser(string email)
        {
            try
            {
                using (var data = new Context(_optionsbuilder))
                {
                    var user = await data.ApplicationUser.
                           Where(x => x.Email.Equals(email))
                           .AsNoTracking()
                           .FirstOrDefaultAsync();

                    return user.Id;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }        
}
