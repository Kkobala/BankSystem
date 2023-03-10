using BankSystem.Auth;
using BankSystem.Db;
using BankSystem.Repositories;
using BankSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContextPool<AppDbContext>(c =>
	c.UseSqlServer(builder.Configuration["DefaultConnection"]));
builder.Services.AddTransient<IAccountRepository,AccountRepository>();

AuthConfigurator.Configure(builder);

builder.Services.AddTransient<ICardRepository, CardRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<TransactionService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
