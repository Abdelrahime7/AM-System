using Application.Interfaces.UserInterfaces;
using Application.Users.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/UsersController")]
    [ApiController]
    public class UsersController(IUserQueries userQueries, IUserCommands userCommands) : ControllerBase
    {
        private readonly IUserQueries _userQueries = userQueries;
        private readonly IUserCommands _userCommands = userCommands;


        [HttpGet(Name = "GetAllUsersAysnc")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsersAysnc()
        {
            var result = await _userQueries.GetAllUsersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return NoContent();
        }

        [HttpGet("{ID}", Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<UserResponse>> GetUserByIDAsync(int id)
        {
            if (id < 1)
            {
                return BadRequest($"Invalid id = {id}");
            }
            var result = await _userQueries.GetUserByIDAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound();
        }




        [HttpPost(Name = "CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<int>> CreateUserAsync([FromBody] CreateUserRequest Request)
        {
           var result = await _userCommands.CreatUserAsync(Request);
            if (result.IsSuccess )
            {
                return CreatedAtRoute($"GetUserByIDAsync", new { Id = result.Value });
            }
            return BadRequest(result.Error);
        }


        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserResponse>> UpdateUserAsync([FromBody] UpdateUserRequest request)
        {

            if (request.Id> 0)
            {
                var result = await _userCommands.UpdateUserAsync(request);
                if (result.IsSuccess)
                {
                    return Ok();
                }

                return NotFound(result.Error);
            }
            return BadRequest("Invalid input.Please check the submitted data");


        }


        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<ActionResult<bool>> DeleteUserAsync(int id)
        {
            if (id < 1)
                return NoContent();
            var result =  await _userCommands.DeleteUserAsync(id);

            if (result.IsSuccess )
            {
                return Ok(result.Value);
            }

            return BadRequest("Customer not deleted");
        }
    }





}

