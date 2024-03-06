using Rockfast.ApiDatabase.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.ServiceInterfaces
{
    public interface IUserService
    {
        Task<User> Save(User user);
        Task<User> Update(User user);
        Task<bool> Delete(int userId);
        Task<List<User>> GetUsers();
    }
}
