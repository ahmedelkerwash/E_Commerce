using E_Commerce.Core.Entities;
using E_Commerce.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Data
{
	public class IdentityDataContextSeed
	{
		public static async Task SeedUserAsync(UserManager<ApplicationUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new ApplicationUser()
				{
					UserName = "Shoura",
					Email = "Shoura@gmail.com",
					DisplayName = "Shour",
					Address = new Address()
					{
						City = "Cairo",
						Country = "Egypt",
						PostalCode = "12345",
						State = "Qalubiya",
						Street = "31"
					}
				};
				await userManager.CreateAsync(user,"P@ssw0rd");
			}
		}
	}
}
