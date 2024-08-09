using Core.Helpers;
using Domain;
using Domain.Scheduler;
using Hangfire;
using Infrastructure;
using Infrastructure.Filters;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


XmlConfigurator.Configure(new FileInfo("log4net.config")); 

builder.Logging.AddLog4Net();

builder.Host.UseSerilog();


Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();
        
builder.InitialSetup();


builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
    });

builder.Services
    .AddAppDbContexts(builder.Configuration.GetConnectionString("DefaultConnection"))
    .AddAppServices()
    .AddAppExternals()
    .AddAppSettings(builder.Configuration)
    .AddAutoMapper(typeof(AutoMapProfile))
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());

    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Seat Management API", Version = "v1" });
    c.EnableAnnotations();

    c.SchemaFilter<SwaggerEnumHelper>();
    c.SchemaFilter<SwaggerExampleHelper>();
});

var cultureInfo = new CultureInfo("id");

CultureInfo.CurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

//builder.Services.AddHangfire(configuration => configuration
//        .SetDataCompatibilityLevel(CompatibilityLevel.Version_110)
//        .UseSimpleAssemblyNameTypeSerializer()
//        .UseRecommendedSerializerSettings()
//        .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddHangfireServer();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DefaultModelsExpandDepth(-1);
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seat Management API");
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}");
    //endpoints.MapHangfireDashboard("/scheduler", new DashboardOptions
    //{
    //    Authorization = new[] { new HangfireAuthFilter(builder.Configuration.GetValue<string>("AppSettings:SchedulerRole")) }
    //});
});

var hangfireJobOpt = new RecurringJobOptions()
{
    TimeZone = TimeZoneInfo.Local
};

//RecurringJob.AddOrUpdate<ISchedulerService>("AssetEndPeriod", m => m.AssetEndPeriod(), "0 1 * * *", hangfireJobOpt);

app.Run();
