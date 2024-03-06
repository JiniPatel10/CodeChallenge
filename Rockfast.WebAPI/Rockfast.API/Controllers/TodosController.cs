#region Usings
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockfast.API.Middleware;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;
#endregion

namespace Rockfast.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        #region Variables
        private readonly ITodoService _todoService;
        private ILogger<TodosController> _logger;
        #endregion

        #region Constructor
        public TodosController(ITodoService todoService, ILogger<TodosController> logger)
        {
            this._todoService = todoService;
            this._logger = logger;
        }
        #endregion

        #region Async Methods
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve todos for user with ID: {userId}");
                var result = await _todoService.GetTodosByUserId(userId);
                _logger.LogInformation($"Successfully retrieved todos for user with ID: {userId}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                string ErrorMsg = "Error getting todos by id" + ex.Message;
                _logger.LogError(ex, ErrorMsg);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.GettingTodoByIdError, ErrorMsg));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Save(TodoVM model)
        {
            try
            {
                _logger.LogInformation("Attempting to create a new todo.");
                var todo = new Todo(model);
                _logger.LogInformation("Todo successfully created in the database.");
                await _todoService.Save(todo);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error occur while creating todo in database " + ex.Message;
                _logger.LogError(ex, errorMessage);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.CreatingTodoError, errorMessage));
            }
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TodoVM model)
        {
            try
            {
                _logger.LogInformation($"Attempting to update todo with ID: {model.Id}");
                var todo = new Todo(model);
                var update = await _todoService.Update(todo);
                _logger.LogInformation($"Todo with ID {model.Id} successfully updated in the database.");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occur while updating todo in database" + ex.Message;
                _logger.LogError(ex, errorMessage);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.CreatingTodoError, errorMessage));
            }
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete todo with ID: {id}");
                var todo = await _todoService.Delete(id);
                _logger.LogInformation($"Todo with ID {id} successfully deleted from the database.");
                return Ok(todo);
            }
            catch (Exception ex)
            {
                string ErrorMsg = "Error deleting todo by id" + ex.Message;
                _logger.LogError(ex, ErrorMsg);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.DeletingTodoError, ErrorMsg));
            }

        }
        #endregion
    }
}
