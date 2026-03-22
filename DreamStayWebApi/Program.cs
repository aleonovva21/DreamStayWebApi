using DreamStay.Domain.AuthTokenProviders;
using DreamStay.Domain.Infrastructure;
using DreamStayWebApi;
using DreamStayWebApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

public partial class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var configuration = builder.Configuration;

		builder.Services.AddControllers().AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow;
		});

		builder.Services.AddDbContext<HostelContext>(options =>
		{
			options.UseNpgsql(configuration.GetConnectionString("DreamStayDB"));
			options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		});

		builder.Services.AddTransient<IAuthTokenProvider, JwtTokenProvider>();
		builder.Services.AddScoped<IValidator<DreamStayWebApi.Models.Rooms.Room>, RoomValidator>();
		builder.Services.AddScoped<IValidator<DreamStayWebApi.Models.Rooms.UpdateRoom>, UpdateRoomValidator>();
		builder.Services.AddOpenApi();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "DreamStay",
				Version = "v1"
			});
			c.CustomSchemaIds(s => s.FullName.Replace("+", "."));
		});

		var requireAuthPolicy = new AuthorizationPolicyBuilder().
			RequireAuthenticatedUser().
			Build();

		builder.Services.
			AddAuthorizationBuilder().
			SetDefaultPolicy(requireAuthPolicy);

		builder.Services.
			AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
			AddJwtBearer(options =>
			{
				var secretKey = configuration.GetSection($"{JwtTokenOptions.Position}:SecretKey").Get<string>();
				var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? string.Empty));

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = key,
					ValidateAudience = false,
					ValidateIssuer = false,
				};
			});


		builder.Services.AddAuthorization();
		builder.Services.Configure<JwtTokenOptions>(configuration.GetSection(JwtTokenOptions.Position));

		var app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(option =>
			{
				option.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			});
			app.MapOpenApi();
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}