#region Usings
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ApiDatabase;
using Rockfast.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
#endregion
namespace Rockfast.Dependencies
{
    public class UserService : IUserService
    {
        #region Variables
        private ApiDbContext _database;
        #endregion

        #region Constructor
        public UserService(ApiDbContext db)
        {
            this._database = db;
        }
        #endregion
        #region Async Methods

        /// <summary>
        /// get all users
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsers()
        {
            var allUsers = await _database.Users.ToListAsync();
            return allUsers;
        }
        /// <summary>
        /// save user in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public async Task<User> Save(User user)
        {
            var result = _database.Users.Add(user);
            await _database.SaveChangesAsync();
            return user;
        }
        /// <summary>
        /// update user in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<User> Update(User model)
        {
            var update = await _database.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (update == null)
                return null;
            update.Name = model.Name;
            var result = await _database.SaveChangesAsync();
            return update;


        }
        /// <summary>
        /// delete user from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int userId)
        {
            var delete = await _database.Users.FirstOrDefaultAsync(x => x.Id == userId);
            _database.Remove(delete);
            await _database.SaveChangesAsync();
            return true;
        }

        #endregion
    }
}
