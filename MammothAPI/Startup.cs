using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MammothAPI.Common;
using MammothAPI.Data;
using MammothAPI.Filters;
using MammothAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MammothAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();
			services.AddHttpContextAccessor();

			var key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:TokenIssuerKey"));
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
			var connection = Configuration.GetConnectionString("MammothDBConnectionString");
			services.AddDbContext<MammothDBContext>(o => o.UseSqlServer(connection));
			services.AddScoped<ISession, Session>();
			services.AddScoped<IAuthenticateService, AuthenticateService>();
			services.AddScoped<IProductsService, ProductsService>();
			services.AddScoped<ISalesService, SalesService>();
			services.AddScoped<IReportsService, ReportsService>();
			services.AddScoped<IMappers, Mappers>();
			services.AddScoped<IReportsService, ReportsService>();
			services.AddSingleton<IEncryptionHelper, EncryptionHelper>();

			services.AddApplicationInsightsTelemetry();
			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//app.UseCorsMiddleware();
			app.UseCors(builder =>
			{
				builder.WithOrigins("http://localhost:4200")
				.AllowAnyMethod()
				.AllowAnyHeader();
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();
			
			//// global cors policy
			//app.UseCors(x => x
			//	.AllowAnyOrigin()
			//	.AllowAnyMethod()
			//	.AllowAnyHeader());
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseMiddleware<ErrorHandlingMiddleware>();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
