using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUser
    {
        Task<bool> AddUser(string email, string password, int age, string phone);
        Task<bool> CheckUser(string email, string password);
        Task<string> ReturnIdUser(string email);
    }
}
