using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Gateway Http Client
var gatewayUrl = builder.Configuration.GetValue<string>("GatewayURL");

builder.Services.AddHttpClient("DefaultClient", client =>
{
    client.BaseAddress = new Uri(gatewayUrl);
});

// For data persistence
//builder.Services.AddDataProtection()
//    .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
