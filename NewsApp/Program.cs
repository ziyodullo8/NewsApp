using Microsoft.EntityFrameworkCore;
using NewsApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conStr = builder.Configuration.GetConnectionString("DefConn");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(conStr));


builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Add}/{id?}");

app.Run();
