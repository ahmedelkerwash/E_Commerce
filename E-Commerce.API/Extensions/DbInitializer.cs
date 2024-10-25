using E_Commerce.Core.Entities.Identity;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.API.Extensions
{
    public static class DbInitializer
	{
		public static async Task InitializeDBAsync(WebApplication app)              
		{
			using (var scope = app.Services.CreateScope())
			{
				var service = scope.ServiceProvider;
				var LoggerFactory = service.GetRequiredService<ILoggerFactory>();

				try
				{
					var context = service.GetRequiredService<DataContext>();
					var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
					

					if ((await context.Database.GetPendingMigrationsAsync()).Any())
						await context.Database.MigrateAsync();

					
					await DataContextSeed.SeedDataAsync(context);
					await IdentityDataContextSeed.SeedUserAsync(userManager);

				}
				catch (Exception ex)
				{
					var logger = LoggerFactory.CreateLogger<Program>();
					logger.LogError(ex.Message);
				}
			}


		}
	}
}
