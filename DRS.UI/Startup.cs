using System;
using System.IO;
using System.Net;
using System.Text;
using DRS.Business;
using DRS.Entities;
using DRS.UI.Helpers;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace DRS.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Asignamos la cadena de conexión del appsetting.json para que la base se pueda conectar.
            ConectionDB.ConnectionString = Configuration["ConnectionStrings:ConnectionString"];
            ConectionDB.Configuration = Configuration;

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x => { 
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            //    {
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            //        ValidateAudience = false,
            //        ValidateIssuerSigningKey = true,
            //        ValidateIssuer = false
            //    };
            //}).AddCookie(options =>
            //{
            //    options.LoginPath = "/Login";
            //    options.LogoutPath = "/Logout";
            //    options.ExpireTimeSpan = new TimeSpan(600000000);
            //    options.AccessDeniedPath = "/Account/Forbidden/";

            //});

            //services.AddAuthentication("CookieAuthentication");
            //services.AddAuthentication();

            //services.AddSingleton<IJwtAuthenticationService>(new JwtAuthenticationService(key));

            //services.AddAutoMapper(typeof(Startup));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("::ffff:0.0.0.0"), 0));
            });

            //Configuramos la autenticación del proyecto.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.LogoutPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromDays(60);

            })
            //.AddJwtBearer(options =>
            //{
            //    options.Audience = "https://devouciones.uc.r.appspot.com/";
            //    options.Authority = "https://devouciones.uc.r.appspot.com/";
            //})

            //.AddGoogle(options =>
            //{
            //    IConfigurationSection googleAuthNSection =
            //    Configuration.GetSection("Authentication:Google");

            //    options.ClientId = googleAuthNSection["ClientId"];
            //    options.ClientSecret = googleAuthNSection["ClientSecret"];
            //})
            ;
            services.AddAuthorization();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.IsEssential = true;
            });

            //services.AddDistributedMemoryCache();

            //services.AddAuthentication("CookieAuthentication")
            //    .AddCookie("CookieAuthentication", config =>
            //    {
            //        config.Cookie.Name = "UserLoginCookie";
            //        //config.LoginPath = "/Home";
            //        //config.LogoutPath = "/Home/Logout";
            //        //config.AccessDeniedPath = "/Home";
            //        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            //        /*config.IdleTimeout = TimeSpan.FromMinutes(60); */                // Set the session expired time               
            //        config.Cookie.HttpOnly = true;
            //        config.Cookie.IsEssential = true;
            //        //config.SlidingExpiration = true;
            //        //config.Events = new CookieAuthenticationEvents()
            //        //{

            //        //}
            //        //config.Events = new CookieAuthenticationEvents()
            //        //{
            //        //    OnRedirectToLogout = ""
            //        //}
            //        //LoginPath = new PathString("/Login/"),

            //        //LogoutPath = new PathString("/Logout/"),
            //        //AccessDeniedPath = new PathString("/Forbidden/"),
            //        //AutomaticAuthenticate = true,
            //        //AutomaticChallenge = true,
            //        //SlidingExpiration = true,
            //        //ExpireTimeSpan = TimeSpan.FromMinutes(30),
            //        //Events = new CookieAuthenticationEvents()
            //        //{
            //        //    OnValidatePrincipal = LoginStatusValidator.ValidateAsync //Just some code to allow deactivated accounts to cause active cookies to be revoked/signed-out
            //        //}
            //        //}).AddJwtBearer(options =>
            //        //{
            //        //    options.Audience = "https://devouciones.uc.r.appspot.com/";
            //        //    options.Authority = "https://devouciones.uc.r.appspot.com/";
            //    })
            //    ;

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromHours(35);                // Set the session expired time               
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/";
                options.LogoutPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
            });

            //ConfigureServices()
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultSignInScheme = "MyAppCookie";
            //});

            //services.AddAntiforgery(
            //    options =>
            //    {
            //        // Rename the Anti-Forgery cookie from "__RequestVerificationToken" to "f". This adds a little
            //        // security through obscurity and also saves sending a few characters over the wire.
            //        options.Cookie.Name = "f";

            //        // Rename the form input name from "__RequestVerificationToken" to "f" for the same reason above
            //        // e.g. <input name="__RequestVerificationToken" type="hidden" value="..." />
            //        options.FormFieldName = "f";

            //        // Rename the Anti-Forgery HTTP header from RequestVerificationToken to X-XSRF-TOKEN. X-XSRF-TOKEN
            //        // is not a standard but a common name given to this HTTP header popularized by Angular.
            //        options.HeaderName = "X-XSRF-TOKEN";
            //        options.Cookie.Expiration = TimeSpan.FromDays(30);
            //    });

            services.AddDataProtection()
            .SetApplicationName("DRS")
            .PersistKeysToFileSystem(new DirectoryInfo(@".\"))
            .SetDefaultKeyLifetime(TimeSpan.FromDays(36500));

            //services.AddOrchardCore()
            //    .AddMvc();

            services.AddControllersWithViews();
            //services.AddHttpContextAccessor();

            // Stop annoying startup messages
            //services.Configure<ConsoleLifetimeOptions>(opts => opts.SuppressStatusMessages = true);
        }
    

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseSession();
            //app.UseCookieAuthentication(opt =>
            //{
            //    opt.AutomaticAuthenticate = false;
            //    opt.AutomaticChallenge = false;
            //    opt.AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllers();
            });

            AppCore.Bootstrap(InjectionType.NetCore);
        }
    }
}
