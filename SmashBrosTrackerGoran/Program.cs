using Microsoft.EntityFrameworkCore;
using SmashBrosTrackerGoran.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SmashDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SmashDB"))
);

var app = builder.Build();

using (var scope = app.Services.CreateScope())//lägger till sample data som characters
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SmashDbContext>();
    SampleData.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
