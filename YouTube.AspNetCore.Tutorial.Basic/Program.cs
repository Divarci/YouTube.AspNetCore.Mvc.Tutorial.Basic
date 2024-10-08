using YouTube.AspNetCore.Tutorial.Basic.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.LoadServicesAndConfigs(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//app.UseExceptionHandler("/Exception/HandleExceptions");
//app.UseStatusCodePagesWithRedirects("/Exception/NotFoundPage");
app.UseStatusCodePagesWithReExecute("/Exception/NotFoundPage");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authenticate}/{action=Login}/{id?}");

app.Run();
