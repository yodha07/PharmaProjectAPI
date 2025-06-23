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

// Add services to the container
builder.Services.AddControllers();
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
builder.Services.AddScoped<ICartRepository, CartService>();
builder.Services.AddScoped<ITransactionRepo, TransactionService>();


// ✅ Configure authentication only once
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.SameSite = SameSiteMode.None; // required for cross-origin
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddCors(options =>

builder.Services.AddScoped<IStockRepo, StockService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>

{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7055") // consuming app
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbconn")));

builder.Services.AddAutoMapper(typeof(MappingData));

var app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend"); // 🔥 Needed for cross-origin
app.UseHttpsRedirection();

app.UseAuthentication(); // 🛡️ Required for [Authorize]
app.UseAuthorization();

app.MapControllers();

app.Run();
