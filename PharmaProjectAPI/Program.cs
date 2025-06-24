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

// Mail config
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MailSettings>>().Value);

// Repositories & Services
builder.Services.AddScoped<IUserRepo, UserService>();
builder.Services.AddScoped<ICustomerRepo, CustomerService>();
builder.Services.AddScoped<ISupplierRepo, SupplierService>();
builder.Services.AddScoped<IMedicine, MedicineService>();
builder.Services.AddScoped<IPurchaseRepo, PurchaseService>();
builder.Services.AddScoped<IDashboard, DashboardService>();
builder.Services.AddScoped<ICartRepository, CartService>();
builder.Services.AddScoped<ITransactionRepo, TransactionService>();
builder.Services.AddScoped<IReportsRepository, ReportService>();
builder.Services.AddScoped<IStockRepo, StockService>();
builder.Services.AddScoped<IOSaleRepo, OSaleService>();
builder.Services.AddScoped<IExpenseRepo, ExpenseService>();


// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

// ✅ CORS policy - fixed location
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7055") // your consuming app
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

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
