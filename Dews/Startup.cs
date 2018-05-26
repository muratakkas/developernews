using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dews.News.Interfaces;
using Dews.News.Managers;
using Dews.News.Entities.NPoco.Manager;
using Dews.DataAccess.NPocoDB.Types;
using Dews.Api.Settings;
using Dews.Api.Constants;
using Dews.Logger.ConsoleLogger.Types;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Dews.Api
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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               // base-address of your identityserver
               options.Authority = Configuration.GetValue<string>(Const.IdpServerUrl);

               // name of the API resource
               options.Audience = Configuration.GetValue<string>(Const.ApiName); 

               options.RequireHttpsMetadata = Configuration.GetValue<bool>(Const.RequireHttpsMetadata); 
           });

            //Set App Settings
            AppSettings appSettings = new AppSettings();
            Configuration.GetSection(Const.AppSettingsKey).Bind(appSettings);
            NPocoEntityManager entityManager = new NPocoEntityManager();
            ConsoleLogger eventViewerLogger = new ConsoleLogger();
            NPocoDatabase nPocoDatabase = new NPocoDatabase(appSettings.ConnectionString);
            services.AddTransient<INewsManager>(c => new NewsManager(nPocoDatabase, entityManager, eventViewerLogger));
            services.AddTransient<ICategoryManager>(c => new CategoryManager(nPocoDatabase, entityManager, eventViewerLogger));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors("MyPolicy");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();


        }
    }
}
