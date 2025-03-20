using CEG_BAL.Configurations;
using CEG_RazorWebApp.Libraries;
using CEG_RazorWebApp.Libraries.Authorizations;
using CEG_RazorWebApp.Services.HostedServices;
using CEG_RazorWebApp.Services.Implements;
using CEG_RazorWebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;

namespace CEG_RazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load secrets.json for local development
            //builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Authentication setup
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
            /*.AddGoogle(options =>
            {
                //IConfigurationSection googleAuthNSection = configuration.GetSection("Authentication:Google");
                //options.ClientId = googleAuthNSection["ClientId"];
                //options.ClientSecret = googleAuthNSection["ClientSecret"];

                options.ClientId = configuration.GetSection(Constants.GOOGLE_CLIENT_ID).Value;
                options.ClientSecret = configuration.GetSection(Constants.GOOGLE_CLIENT_SECRET).Value;
                options.CallbackPath = Constants.GOOGLE_REDIRECT_URI_PATH;
                options.SaveTokens = true;
            })*/
            ;

            // Add Azure Storage configuration
            //services.Configure<AzureStorageConfig>(configuration.GetSection("AzureStorage"));

            // Add HttpClient
            services.AddHttpClient();

            services.AddRazorPages(options =>
            {
                // options.Conventions.AuthorizeFolder("/Admin", "SessionAuthorize");
                // options.Conventions.AddPageRoute("/Home/Index", "/Index");
            });

            // Add controllers with views
            /*services.AddControllersWithViews();*/

            // Add AutoMapper
            services.AddAutoMapper(typeof(WebVMMappingProfile));

            // Add session configuration
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
            });

            /*services.AddHttpContextAccessor();

            services.AddAuthorizationBuilder()
                .AddPolicy("SessionAuthorize", policy =>
                {
                    policy.Requirements.Add(new CEGAuthorizeRequirement());
                });

            *//*services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();*//*
            services.AddScoped<IAuthorizationHandler, CEGAuthorizeHandler>();*/

            /*services.AddAuthorization(options =>
            {
                options.AddPolicy("SessionAuthorize", policy =>
                {
                    policy.Requirements.Add(new CEGAuthorizeRequirement());
                });
            });*/
            // Add scoped services
            services.AddScoped<ISystemLoginService, SystemLoginService>();
            services.AddScoped<IVnPayService, VnPayService>();

            // Add Hosted services
            services.AddHostedService<ChangeClassStatusService>();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // Configure exception handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // HSTS middleware
            }

            // Enable HTTPS redirection and static files
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Enable session management
            app.UseSession();

            // Enable CORS
            /*app.UseCors();*/

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure endpoint routing
            /*app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");*/
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Home/Index");
                return Task.CompletedTask;
            });
            app.MapRazorPages();
        }

        /*public static void LoadConfiguration(WebApplicationBuilder builder)
        {
            // Optionally load secrets from Azure Key Vault or secrets.json
            var configuration = builder.Configuration;

            // Load user secrets for local development
            builder.Configuration.AddUserSecrets<Program>();

            // Optional: Load Azure Key Vault
            string keyVaultName = configuration["KeyVaultName"];
            if (!string.IsNullOrEmpty(keyVaultName))
            {
                var keyVaultUri = new Uri($"https://{keyVaultName}.vault.azure.net/");
                builder.Configuration.AddAzureKeyVault(keyVaultUri, new Azure.Identity.DefaultAzureCredential());
            }
        }*/
    }
}