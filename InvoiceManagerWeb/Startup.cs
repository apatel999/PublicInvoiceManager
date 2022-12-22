using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using DAL;
using AutoMapper;
using InvoiceManager.ViewModels;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using System.Text;
using Newtonsoft.Json.Serialization;
using InvoiceManagerWeb.ViewModel;
using Microsoft.Extensions.Localization;
using System.Globalization;
using DAL.Services;

namespace InvoiceManagerWeb
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
            
            services.AddMvc();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            // Enable Cors
            services.AddCors(o => o.AddPolicy("IMCORSPolicy", builder =>{
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddLogging();
            services.AddTransient<IDatabaseOptions>(s=> new DatabaseOptions(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
            services.AddTransient<ISalesReportService, SalesReportService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            cultureInfo.DateTimeFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm:ss";
            cultureInfo.DateTimeFormat.DateSeparator = "-";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UsePathBase("/api");
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseCors("IMCORSPolicy");

            //TODO: Add global exception handler.

            //loggerFactory.AddFile("apilog/myapp-{Date}.txt");

            app.UseExceptionHandler(
              builder =>
              {
                  builder.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.Headers.Add("Application-Error", error.Error.Message);
                            // CORS
                            context.Response.Headers.Add("access-control-expose-headers", "Application-Error");

                            var bytes = Encoding.ASCII.GetBytes(error.Error.Message);
                            await context.Response.Body.WriteAsync(bytes,0,bytes.Length).ConfigureAwait(false);
                        }
                    });
              });

            app.UseMvc();
        }
    }
}
