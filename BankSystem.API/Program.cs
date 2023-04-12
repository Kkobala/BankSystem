using BankSystem.Auth;
using BankSystem.Db;
using BankSystem.Middlewares;
using BankSystem.Repositories.Implementations;
using BankSystem.Repositories.Interfaces;
using BankSystem.Services.Implementations;
using BankSystem.Services.Interfaces;
using BankSystem.Validations.Implementation;
using BankSystem.Validations.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Services.AddLogging(options =>
{
	options.AddSerilog(dispose: true);
});
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(builder.Configuration)
	.CreateLogger();
builder.Logging.AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddDbContextPool<AppDbContext>(c =>
	c.UseSqlServer(builder.Configuration["DefaultConnection"]));

AuthConfigurator.Configure(builder);

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IATMRepository, ATMRepository>();
builder.Services.AddScoped<IATMService, ATMService>();
builder.Services.AddScoped<IConverterService, ConverterService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IBankSystemValidations, BankSystemValidations>();

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "JWTToken_Auth_API",
		Version = "v1"
	});
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\\r\\n\\r\\nExample: \"Bearer1safsfsdfdfd\"\"",
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
