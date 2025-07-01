using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagementSystem.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var dtos = users.Select(u => new UserDto(u)).ToList();
                return Ok(dtos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();
                return Ok(new UserDto(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var user = dto.ToUser();
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, new UserDto(user));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            try
            {
                if (id != dto.Id) return BadRequest();
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();
                dto.UpdateUser(user);
                await _userService.UpdateUserAsync(user);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new { message = "A database error occurred.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }
    }
} 