// *****************************************************************************************************************
// Project          : NavyBlue
// File             : Startup.cs
// Created          : 2019-01-10  18:53
//
// Last Modified By : (jstsmaxx@163.com)
// Last Modified On : 2019-01-10  19:26
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
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using NavyBlue.AspNetCore.Web.Middlewares;
using NavyBlue.AspNetCore.Web;

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

            app.UseTraceEntry();
            app.UseJsonResponseWapper();
            //app.UseNBAuthorization();

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(p =>
            {
                p.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
                //AppContext.BaseDirectory
                p.IncludeXmlComments(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "NavyBlue.Demo.API.xml"));
            });
        }
    }
}