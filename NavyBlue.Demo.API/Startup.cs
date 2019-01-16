// *****************************************************************************************************************
// Project          : NavyBlue
// File             : Startup.cs
// Created          : 2019-01-10  18:53
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-15  10:55
// *****************************************************************************************************************
// <copyright file="Startup.cs" company="Shanghai Future Mdt InfoTech Ltd.">
//     Copyright ©  2012-2019 Mdt InfoTech Ltd. All rights reserved.
// </copyright>
// *****************************************************************************************************************

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NavyBlue.AspNetCore.Web.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using NavyBlue.AspNetCore.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NavyBlue.AspNetCore;

namespace NavyBlue.Demo.API
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(configuration..ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddEnvironmentVariables();
            //Configuration = builder.Build();

            this.Configuration = configuration;
        }

        /// <summary>
        ///     Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            object httpContext = app.ApplicationServices.GetService(typeof(IHttpContextAccessor));

            App.Initialize().InitConfig(app.ApplicationServices.GetService<IOptions<AppConfig>>()).UseGovernmentServerConfigManager<DemoConfig>(); //original
            string bearerAuthKeys = App.Configurations.GetConfig<DemoConfig>().BearerAuthKeys.HtmlDecode();
            string governmentServerPublicKey = App.Configurations.GetConfig<DemoConfig>().GovernmentServerPublicKey.HtmlDecode();

            app.UseTraceEntry();
            //app.UseNBAuthorization(((HttpContextAccessor)httpContext).HttpContext, bearerAuthKeys, governmentServerPublicKey);
            app.UseExceptionHandling();
            app.UseJsonResponseWapper();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(p => { p.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        }

        /// <summary>
        ///     This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
                p.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "NavyBlue.Demo.API.xml"));
            });
        }
    }
}