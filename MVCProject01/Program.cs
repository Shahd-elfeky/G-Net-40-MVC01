using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCProject01.Data;
using MVCProject01.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GymDbContext>();
    context.Database.Migrate();

    var plans = new List<Plan>
    {
        new()
        {
            Name = "Basic Plan",
            Price = 300,
            DurationInDays = 30,
            Description = "Access to gym equipment during staffed hours",
            IsActive = true
        },
        new()
        {
            Name = "Standard Plan",
            Price = 450,
            DurationInDays = 60,
            Description = "Access to gym equipment with group classes",
            IsActive = true
        },
        new()
        {
            Name = "Premium Plan",
            Price = 600,
            DurationInDays = 90,
            Description = "All access with personal training sessions",
            IsActive = true
        }
    };

    foreach (var plan in plans)
    {
        var existingPlan = context.Plans.FirstOrDefault(item => item.Name == plan.Name);

        if (existingPlan is null)
        {
            context.Plans.Add(plan);
        }
        else
        {
            existingPlan.Price = plan.Price;
            existingPlan.DurationInDays = plan.DurationInDays;
            existingPlan.Description = plan.Description;
            existingPlan.IsActive = plan.IsActive;
        }
    }

    context.SaveChanges();
}

app.Run();
