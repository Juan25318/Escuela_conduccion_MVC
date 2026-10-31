using MVC_Seguimiento3.Services;

var builder = WebApplication.CreateBuilder(args);

// Habilita controladores y vistas Razor MVC
builder.Services.AddControllersWithViews();

// Servicio en memoria
builder.Services.AddSingleton<EscuelaService>();


var puerto = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(puerto))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{puerto}");
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
