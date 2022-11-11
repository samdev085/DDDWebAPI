using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Applications
{
    public class ApplicationUser : IApplicationUser
    {
        IUser _IUser;

        public ApplicationUser(IUser IUser)
        {
            _IUser = IUser;
        }


        public async Task<bool> AddUser(string email, string password, int age, string phone)
        {
            return await _IUser.AddUser(email, password, age, phone);
        }

        public async Task<bool> CheckUser(string email, string password)
        {
            return await _IUser.CheckUser(email, password);
        }

        public async Task<string> ReturnIdUser(string email)
        {
            return await _IUser.ReturnIdUser(email);
        }
    }
}
