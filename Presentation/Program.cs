using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Services.Interfaces;
using Services.Repositories;

void SetDataDirectory()
{
    string projectDir = Directory.GetCurrentDirectory();
    int IndexOfLastSlash = projectDir.LastIndexOf("\\");
    string solutionDir = projectDir.Substring(0, IndexOfLastSlash);
    string path = Path.Combine(solutionDir, "Infrastructure\\DB");
    AppDomain.CurrentDomain.SetData("DataDirectory", path);
}

void ConfigureServices(WebApplicationBuilder builder)
{
    string connection = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<UniversityDbContext>(options => options.UseSqlServer(connection));
    builder.Services.AddScoped<IUnitOfWork<UniversityDbContext>, UnitOfWork<UniversityDbContext>>();
    builder.Services.AddTransient<ICoursesRepository, CoursesRepository>();
    builder.Services.AddTransient<IGroupsRepository, GroupsRepository>();
    builder.Services.AddTransient<IStudentsRepository, StudentsRepository>();
}

SetDataDirectory();

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

string appName = app.Environment.ApplicationName;


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
