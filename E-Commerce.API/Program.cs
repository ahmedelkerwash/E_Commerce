using E_Commerce.API.Errors;
using E_Commerce.API.Extensions;
using E_Commerce.API.Helper;
using E_Commerce.Core.Entities.Identity;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Data.DataSeeding;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Reflection;

namespace E_Commerce.API
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			

			var builder = WebApplication.CreateBuilder(args);


			builder.Services.AddControllers();
			builder.Services.AddSwaggerService();


			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddIdentityService(builder.Configuration);

			

			var app = builder.Build();
			await DbInitializer.InitializeDBAsync(app);

			#region Pipelines / Middlewares
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseStaticFiles();
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			app.UseMiddleware<CustomExceptionHandler>();     // for the internal server exception

			app.Run();

			#endregion
		}

		
	}
}
