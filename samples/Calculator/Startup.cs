﻿using System.Reflection;
using Calculator.Contract;
using Calculator.Handler;
using Calculator.WebService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using XRoadLib;
using XRoadLib.Extensions;
using XRoadLib.Headers;
using XRoadLib.Schema;

namespace Calculator
{
    public class Startup
    {
        private readonly IServiceManager serviceManager = new ServiceManager<XRoadHeader40>("4.0", new DefaultSchemaExporter("http://calculator.x-road.eu/", typeof(Startup).GetTypeInfo().Assembly));

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICalculate, CalculateWebService>();
            services.AddSingleton<ISumOfIntegers, SumOfIntegersWebService>();
            services.AddSingleton(provider => new CalculatorHandler(provider, new[] { serviceManager }, null));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseXRoadLib(options =>
            {
                options.AddRequestHandler<CalculatorHandler>();
                options.ServiceManagers.Add(serviceManager);
            });

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
