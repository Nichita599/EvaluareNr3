using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Adaug? serviciile pentru DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adaug? serviciile MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configureaz? rutele
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");


app.Run();
