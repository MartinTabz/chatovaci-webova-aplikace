using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Talkero.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Propojení databáze connections stringem z Program.cs
builder.Services.AddDbContext<Talkero.Data.TalkeroData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TalkeroDB")));

// Cookie při přihlášení (Jestli doopravdu funguje je záhada)
builder.Services.AddSession(options => {
    options.Cookie.Name = ".Talkero";
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
