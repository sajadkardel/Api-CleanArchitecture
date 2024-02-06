using Autofac;
using Domain.Settings;
using Infrastructure.Configuration;
using Infrastructure.Middlewares;
using Infrastructure.PackageConfiguration.Autofac;
using Infrastructure.PackageConfiguration.AutoMapper;
using Infrastructure.PackageConfiguration.Identity;
using Infrastructure.PackageConfiguration.Swagger;

namespace Api;

public class Startup
{
    private readonly GeneralSettings _generalSettings;
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;

        _generalSettings = configuration.GetSection(nameof(GeneralSettings)).Get<GeneralSettings>();
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<GeneralSettings>(Configuration.GetSection(nameof(GeneralSettings)));

        services.InitializeAutoMapper();

        services.AddDbContext(Configuration);

        services.AddCustomIdentity(_generalSettings.IdentitySettings);

        services.AddMinimalMvc();

        services.AddElmahCore(Configuration, _generalSettings);

        services.AddJwtAuthentication(_generalSettings.JwtSettings);

        services.AddCustomApiVersioning();

        services.AddSwagger();

        // Don't create a ContainerBuilder for Autofac here, and don't call builder.Populate()
        // That happens in the AutofacServiceProviderFactory for you.
    }

    // ConfigureContainer is where you can register things directly with Autofac. 
    // This runs after ConfigureServices so the things ere will override registrations made in ConfigureServices.
    // Don't build the container; that gets done for you by the factory.
    public void ConfigureContainer(ContainerBuilder builder)
    {
        //Register Services to Autofac ContainerBuilder
        builder.AddServices();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.InitializeDatabase();

        app.UseCustomExceptionHandler();

        app.UseHsts(env);

        app.UseHttpsRedirection();

        app.UseElmahCore(_generalSettings);

        app.UseSwaggerAndUI();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        //Use this config just in Develoment (not in Production)
        //app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

        app.UseEndpoints(config =>
        {
            config.MapControllers(); // Map attribute routing
            //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
            //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
        });

        //Using 'UseMvc' to configure MVC is not supported while using Endpoint Routing.
        //To continue using 'UseMvc', please set 'MvcOptions.EnableEndpointRouting = false' inside 'ConfigureServices'.
        //app.UseMvc();
    }
}
