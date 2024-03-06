#region Usings
using Microsoft.EntityFrameworkCore;
using Rockfast.ApiDatabase;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion
namespace Rockfast.Dependencies
{
    public class TodoService : ITodoService
    {
        #region Variables
        private ApiDbContext _database;
        #endregion

        #region Constructor
        public TodoService(ApiDbContext db)
        {
            this._database = db;
        }
        #endregion
        #region Async Methods
        /// <summary>
        /// get todos by userid from database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task<List<Todo>> GetTodosByUserId(int userId)
        {
            var allTodos = await _database.Todos.Where(x => x.UserId == userId).ToListAsync();
            return allTodos;
        }
        /// <summary>
        /// save todo in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>


        public async Task<Todo> Save(Todo todo)
        {
            var result = _database.Todos.Add(todo);
            await _database.SaveChangesAsync();
            return todo;
        }
        /// <summary>
        /// update todo in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public async Task<Todo> Update(Todo model)
        {
                var update = await _database.Todos.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (update == null)
                    return null;
                update.Name = model.Name;
                update.DateCreated = model.DateCreated;
                update.DateCompleted = model.DateCompleted;
                update.Complete = model.Complete;
            update.UserId = model.UserId;
                var result = await _database.SaveChangesAsync();
                return update;


        }
        /// <summary>
        /// delete todo from database
        /// </summary>
        /// <param name="todoId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int todoId)
        {
            var delete = await _database.Todos.FirstOrDefaultAsync(x => x.Id == todoId);
            _database.Remove(delete);
            await _database.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
