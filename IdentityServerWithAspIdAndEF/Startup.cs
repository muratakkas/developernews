using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerWithAspIdAndEF.Data;
using IdentityServerWithAspIdAndEF.Models;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;
using IdentityServer4;
using IdentityServerWithAspIdAndEF.Services;
using IdentityServerWithAspIdAndEF.ActionFilter;
using IdentityServerWithAspIdAndEF.Extensions;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection;
using System.IO;

namespace IdentityServerWithAspIdAndEF
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicy", buildeCors =>
            {
                #if DEBUG
                buildeCors.AllowAnyOrigin();
                #else 
                buildeCors.WithOrigins(new string[] { "http://developernews.site" });
                #endif 
                buildeCors.AllowAnyMethod();
                buildeCors.AllowAnyHeader();
            }));


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ValidateReCaptchaAttribute>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<ApplicationUser>()
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    // options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                });
            services.Configure<KeyManagementOptions>(options =>
            {
                options.XmlRepository = new MyCustomXmlRepository();
            });

           

            #if DEBUG
                        builder.AddDeveloperSigningCredential();
            #else

                        services.AddDataProtection()
                        .PersistKeysToFileSystem(new DirectoryInfo(Configuration.GetSection("SigninKeyCredentials").GetValue<string>(SigninCredentialExtension.KeyPersistPath)));

                        builder.AddSigninCredentialFromConfig(Configuration.GetSection("SigninKeyCredentials"));
            #endif



            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.ClientId = Configuration.GetSection("Google").GetValue<string>("ClientId");
                    options.ClientSecret = Configuration.GetSection("Google").GetValue<string>("ClientSecret");
                }).AddTwitter(options =>
                {
                    options.ConsumerKey = Configuration.GetSection("Twitter").GetValue<string>("ClientId");
                    options.ConsumerSecret = Configuration.GetSection("Twitter").GetValue<string>("ClientSecret");
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            // Enable Cors
            app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
