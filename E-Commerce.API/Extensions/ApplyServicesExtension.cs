using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.Interfaces;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace E_Commerce.API.Extensions
{
	public static class ApplyServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services , IConfiguration configuration)
		{
			services
				.AddDbContext<DataContext>(o => o.UseSqlServer(configuration.GetConnectionString("SQLConnection")));

			services
				.AddDbContext<IdentityDataContext>(o => o.UseSqlServer(configuration.GetConnectionString("IdentitySQLConnection")));
			
			services.AddScoped<IProductService, ProductService>();    
			services.AddScoped<IUnitOfWork, UnitOfWork>();            
			
			
			services.AddAutoMapper(m => m.AddProfile(new MappingProfile())); 
			services.AddScoped<PictureUrlResolver>();
			
			
			services.AddScoped<IBasketService, BasketServices>();     
			services.AddScoped<IBasketRepository, BasketRepository>(); 
			services.AddScoped<ICashService, CashService>();          
			
			
			services.AddSingleton<IConnectionMultiplexer>(opt =>
			{
				var config = ConfigurationOptions.Parse(configuration.GetConnectionString("RadisConnection"));     
				return ConnectionMultiplexer.Connect(config);
			});


			services.Configure<ApiBehaviorOptions>(option =>
			{
				option.InvalidModelStateResponseFactory = context =>
				{
					var errors = context.ModelState.Where(e => e.Value.Errors.Any()).SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();

					return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = errors });
				};
			});

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();

			return services;
		}
	}
}
