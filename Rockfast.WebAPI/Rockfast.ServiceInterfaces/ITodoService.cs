using Rockfast.ApiDatabase.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rockfast.ServiceInterfaces
{
    public interface ITodoService
    {
        Task<Todo> Save(Todo todo);
        Task<Todo> Update(Todo model);
        Task<bool> Delete(int todoId);
        Task<List<Todo>> GetTodosByUserId(int userId);
    }
}
