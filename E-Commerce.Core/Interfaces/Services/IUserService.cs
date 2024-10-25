using E_Commerce.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Services
{
	public interface IUserService
	{
		public Task<UserDTO?> LoginAsync(LoginDTO dto);
		public Task<UserDTO> RegisterAsync(RegisterDTO dto);
	}
}
