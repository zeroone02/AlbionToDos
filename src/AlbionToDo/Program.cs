using AlbionToDo.Application;
using AlbionToDo.Application.Abstractions.Repositories;
using AlbionToDo.Application.Abstractions.Services;
using AlbionToDo.EFCore;
using AlbionToDo.EFCore.Repositories;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services, builder.Configuration);

        var app = builder.Build();

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
    }
    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureEntityFrameworkCore(services, configuration);
        ConfigureApplicationServices(services);

        services.AddControllersWithViews();
    }

    private static void ConfigureApplicationServices(IServiceCollection services)
    {
        services.AddTransient<IToDoTaskService, ToDoTaskService>();
        services.AddTransient<IToDoExcelService, ToDoExcelService>();
    }

    private static void ConfigureEntityFrameworkCore(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPostgresqlMigrationRunner(configuration);
        services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
    }
}

