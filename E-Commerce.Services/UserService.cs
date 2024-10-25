using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ITokenService _tokenService;

		public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenService tokenService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public async Task<UserDTO?> LoginAsync(LoginDTO dto)
		{

			// Email , has user ? has the password provided ? then login(create token and return dto ) : else return null

			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user is not null)
			{
				var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
				if (result.Succeeded)
				{
					return new UserDTO()
					{
						DisplayName = user.DisplayName,
						Email = user.Email,
						Token = _tokenService.GenerateToken(user)
					};
				}
			}
			return null;

			throw new NotImplementedException();
		}

		public async Task<UserDTO> RegisterAsync(RegisterDTO dto)
		{
			var user = await _userManager.FindByEmailAsync(dto.Email);
			if (user is not null)
				throw new Exception("Email Exists");
			var appUser = new ApplicationUser()
			{
				Email = dto.Email,
				DisplayName = dto.DisplayName,
				UserName = dto.DisplayName,
			};
			var result = await _userManager.CreateAsync(appUser, dto.Password);
			if (!result.Succeeded)
				new Exception("Erroooooor");

			return new UserDTO()
			{
				DisplayName = appUser.DisplayName,
				Email = appUser.Email,
				Token = _tokenService.GenerateToken(appUser)
			};
		}
	}
}
