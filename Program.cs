using Rotativa.AspNetCore;
using DoctorScheduleApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();


var app = builder.Build();

RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

Rotativa.AspNetCore.RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Export}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
