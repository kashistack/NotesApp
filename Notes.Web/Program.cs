
using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces;
using Notes.Infrastructure.Data;
using Notes.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var conn = builder.Configuration.GetConnectionString("NotesDatabase") ?? "Server=localhost,1433;Database=NotesAppDb;User Id=sa;Password=Your@Password123;TrustServerCertificate=True";
builder.Services.AddDbContext<NotesDbContext>(options => options.UseSqlServer(conn));

builder.Services.AddScoped<INoteService, NoteService>();

var app = builder.Build();

// Ensure DB exists (will create with EnsureCreated if migration not present)
app.Services.EnsureDatabaseCreated();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Notes}/{action=Index}/{id?}");

app.Run();
