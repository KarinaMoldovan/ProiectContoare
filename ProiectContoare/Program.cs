using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProiectContoare.Data;
using Microsoft.AspNetCore.Identity;
using ProiectContoare.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy =>
       policy.RequireRole("Admin"));
    });


// Add services to the container.
builder.Services.AddRazorPages(
    options =>
    {
        options.Conventions.AuthorizeFolder("/Consumatori");
        options.Conventions.AuthorizeFolder("/Facturi");
        options.Conventions.AuthorizeFolder("/Plati");
        options.Conventions.AuthorizeFolder("/Contoare");
        options.Conventions.AllowAnonymousToPage("/");
        options.Conventions.AllowAnonymousToPage("/Tarife");
        options.Conventions.AuthorizeFolder("/Consumatori", "AdminPolicy");

    });

builder.Services.AddDbContext<ProiectContoareContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectContoareContext") ?? throw new InvalidOperationException("Connection string 'ProiectContoareContext' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LibraryIdentityContext>();

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProiectContoareContext") ?? throw new InvalidOperationException("Connection string 'ProiectContoareContext' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LibraryIdentityContext>();


var app = builder.Build();

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
