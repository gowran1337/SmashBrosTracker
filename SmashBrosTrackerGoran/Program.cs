using Microsoft.EntityFrameworkCore;
using SmashBrosTrackerGoran.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SmashDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SmashDB"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())//l�gger till sample data som characters
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SmashDbContext>();
    SampleData.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
