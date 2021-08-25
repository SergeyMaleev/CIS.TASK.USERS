using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("/[controller]")]        
    [ApiController]   
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleServices _roleServices;

        public UsersController(IUserService userService, IRoleServices roleServices)
        {
            _userService = userService;
            _roleServices = roleServices;

        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<UserModels>>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            return users != null
                ? Ok(users)
                : NoContent();
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserModels>> GetUser(int id)
        {
            var user = await _userService.FindByIdAsync(id);

            return user != null
                ? Ok(user)
                : NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Администратор")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<UserModels>> PutUser(int id, UserRequestModels user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var result = _userService.UpdateUserAsync(user);

            return result != null
                ? await GetUser(id)
                : NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserModels>> PostUser(UserRequestModels user)
        {
            var result = await _userService.AddUserAsync(user);

            return result > 0
                ? await GetUser(result)
                : BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Администратор")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]      
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id.ToString() == User.Identity.Name)
                return BadRequest();

            var result = await _userService.RemoveUserAsync(id);

            return result
                ? Ok()
                : NotFound();
        }

        [HttpGet("getAvailableRoles/")]
        [Authorize]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        public async Task<ActionResult<IEnumerable<RoleModels>>> GetAvailableRoles()
        {
            return Ok(await _roleServices.GetAvailableRoles());
        }

        [HttpGet("addRole/{userId}/{roleId}")]
        [Authorize(Roles = "Администратор")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<RoleModels>>> AddUserRole(int userId, int roleId)
        {
            var result = await _roleServices.AddUserRole(userId, roleId);

            return result != null
                ? Ok(result)
                : BadRequest();
        }

        [HttpGet("deleteRole/{userId}/{roleId}")]
        [Authorize(Roles = "Администратор")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<RoleModels>>> DeleteUserRole(int userId, int roleId)
        {
            var result = await _roleServices.DeleteUserRole(userId, roleId);

            return result != null
                ? Ok(result)
                : BadRequest();
        }
    }
}
