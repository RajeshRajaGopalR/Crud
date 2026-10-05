using Microsoft.EntityFrameworkCore;
using WebApplication_CRUD.Data;
using WebApplication_CRUD.Repository;
using WebApplication_CRUD.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<IProductRepository,
                           ProductRepository>();

// Service
builder.Services.AddScoped<IProductService,
                           ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductDtoes}/{action=Index}/{id?}");

app.Run();