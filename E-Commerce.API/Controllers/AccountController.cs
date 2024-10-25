using E_Commerce.API.Errors;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
	[Route("api/[controller]/[Action]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;

		public AccountController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO dto)
		{
			var user = await _userService.LoginAsync(dto);
			return user is not null ? Ok(user) : Unauthorized(new ApiResponse(401 , "Incorect email or password"));

		}
		[HttpPost]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO dto)
		{
			return Ok(await _userService.RegisterAsync(dto));
		}
	}
}
