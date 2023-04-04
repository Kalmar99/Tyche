using Tyche.StarterApp;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages(options =>
{
    options.RootDirectory = "/";
});

builder.Services.AddComponents(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapRazorPages();

app.MapFallbackToFile("index.html");


app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }