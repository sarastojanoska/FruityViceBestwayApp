using FruityViceBestwayApp.Data;
using FruityViceBestwayApp.DataRepository;
using FruityViceBestwayApp.Mappings;
using FruityViceBestwayApp.Models.Helper;
using FruityViceBestwayApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<ExceptionDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ExceptionDatabase")));

builder.Services.AddScoped<IFruitViceService,FruitViceService>();
builder.Services.AddScoped<IAPIFruityService,APIFruityService>();
builder.Services.AddScoped<IExceptionLoggerService,ExceptionLoggerService>();
builder.Services.AddScoped<IExceptionLoggerRepository,ExceptionLoggerRepository>();
builder.Services.AddScoped<IFruitViceRepository,FruitViceRepository>();
builder.Services.Configure<FruitViceConfig>(builder.Configuration.GetSection("FruitViceConfig"));
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile)); // Adjust the type parameter as needed


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
