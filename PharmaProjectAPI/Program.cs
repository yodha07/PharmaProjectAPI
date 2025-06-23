using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PharmaProjectAPI.Data;
using PharmaProjectAPI.Mapping;
using PharmaProjectAPI.Models;
using PharmaProjectAPI.Repository;
using PharmaProjectAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MailSettings>>().Value);

builder.Services.AddScoped<IUserRepo, UserService>();

builder.Services.AddScoped<ICustomerRepo, CustomerService>();

builder.Services.AddScoped<ISupplierRepo, SupplierService>();
builder.Services.AddScoped<IMedicine, MedicineService>();

builder.Services.AddScoped<IPurchaseRepo, PurchaseService>();

builder.Services.AddScoped<IDashboard, DashboardService>();
builder.Services.AddScoped<ICartRepository, CartService >();
builder.Services.AddScoped<ITransactionRepo, TransactionService>();

builder.Services.AddScoped<IStockRepo, StockService>();
builder.Services.AddScoped<IOSaleRepo, OSaleService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));
builder.Services.AddAutoMapper(typeof(MappingData));

builder.Services.AddScoped<IMedicine, MedicineService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
