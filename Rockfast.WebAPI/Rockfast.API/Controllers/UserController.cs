#region Usings
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rockfast.API.Middleware;
using Rockfast.ApiDatabase.DomainModels;
using Rockfast.Dependencies;
using Rockfast.ServiceInterfaces;
using Rockfast.ViewModels;
#endregion
namespace Rockfast.API.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Variables
        private readonly IUserService _userService;
        private ILogger<UserController> _logger;
        #endregion

        #region Constuctor

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this._userService = userService;
            this._logger = logger;
        }
        #endregion

        #region Async Methods
        [HttpGet("getallusers")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation($"Attempting to retrieve all users");
                var result = await _userService.GetUsers();
                _logger.LogInformation($"Successfully retrieved all users");
                return Ok(result);
            }
            catch (Exception ex)
            {
                string ErrorMsg = "Error getting users" + ex.Message;
                _logger.LogError(ex, ErrorMsg);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.GettingUsers, ErrorMsg));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserVM model)
        {
            try
            {
                _logger.LogInformation("Attempting to create a new user.");
                var user = new User(model);
                _logger.LogInformation("User successfully created in the database.");
                await _userService.Save(user);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error occur while creating user in database " + ex.Message;
                _logger.LogError(ex, errorMessage);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.CreatingUserError, errorMessage));
            }
            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UserVM model)
        {
            try
            {
                _logger.LogInformation($"Attempting to update user with ID: {model.Id}");
                var user = new User(model);
                var update = await _userService.Update(user);
                _logger.LogInformation($"User with ID {model.Id} successfully updated in the database.");
            }
            catch (Exception ex)
            {
                string errorMessage = "Error occur while updating user in database" + ex.Message;
                _logger.LogError(ex, errorMessage);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.UpdatingUserError, errorMessage));
            }
            return Ok(model);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to delete user with ID: {id}");
                var user = await _userService.Delete(id);
                _logger.LogInformation($"User with ID {id} successfully deleted from the database.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                string ErrorMsg = "Error deleting user by id" + ex.Message;
                _logger.LogError(ex, ErrorMsg);
                return BadRequest(new GeneralErrorResultModel(ErrorCodes.DeletingUserError, ErrorMsg));
            }

        }
        #endregion
    }

}

