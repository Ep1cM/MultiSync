using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MultiSync;
using MultiSync.Data;
using MultiSync.Models.Item;
using MultiSync.Repository.MongoDB;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;
using MultiSync.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// MSSQL configuration
string? connection = builder.Configuration.GetConnectionString("MSConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
builder.Services.AddScoped<IMSRepo<MSItem>, MSRepo>();


// MongoDB configuration
string? connectionMongoDB = builder.Configuration.GetConnectionString("MongoDBConnection");
string? dataMongoDB = builder.Configuration.GetConnectionString("MongoDBDatabaseName");
builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionMongoDB));
builder.Services.AddSingleton<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase(dataMongoDB));
builder.Services.AddScoped<IMongoDBRepo<BsonItem>, MongoDBRepo>();

builder.Services.AddSingleton<EventManager>();
builder.Services.AddScoped<EventHandlerService>();

// XML Configuration
builder.Services.AddSingleton<IXMLRepo<XMLItem>, XMLRepo>();

//Map all att once
builder.Services.AddAutoMapper(typeof(AppMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
