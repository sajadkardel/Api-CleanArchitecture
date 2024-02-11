using Domain.Settings;
using Infrastructure.Configuration;
using Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<GeneralSettings>(builder.Configuration.GetSection(nameof(GeneralSettings)));
builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddMinimalMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHsts(app.Environment);

app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//Use this config just in Develoment (not in Production)
app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//app.UseEndpoints(config =>
//{
//    config.MapControllers(); // Map attribute routing
//                             //    .RequireAuthorization(); Apply AuthorizeFilter as global filter to all endpoints
//                             //config.MapDefaultControllerRoute(); // Map default route {controller=Home}/{action=Index}/{id?}
//});

app.Run();