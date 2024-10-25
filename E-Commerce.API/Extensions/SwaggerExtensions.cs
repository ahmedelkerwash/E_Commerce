using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace E_Commerce.API.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwaggerService(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			//services.AddSwaggerGen(options =>
			//{
			//	var scheme = new OpenApiSecurityScheme()
			//	{
			//		Description = "Standard Authorization header using the bearer scheme",
			//		In = ParameterLocation.Header,
			//		Name = "Authorization",
			//		Type = SecuritySchemeType.ApiKey,
			//	};

			//	options.AddSecurityDefinition("Bearer" , scheme);
			//	options.OperationFilter<SecurityRequirementsOperationFilter>();
			//});

			return services;
		}


	}
}
