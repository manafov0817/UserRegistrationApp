using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RabbitMq.Common;
using RabbitMQ.Client;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserRegistirationApp.Data.Abstract;
using UserRegistirationApp.Data.Concrete.Ado.Net;
using UserRegistirationApp.WebUi.BackgroundTasks;
using RabbitMq.Common.IntegrationEvent;
using UserRegistirationApp.WebUi.Mapper;
using UserRegistirationApp.WebUi.IntegrationEvents;
using UserRegistirationApp.WebUi.Producers;

namespace UserRegistirationApp.WebUi
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
            services
                 //.AddHostedService<>()
                 .AddSingleton<IRabbitMqProducer<UserIntegrationEvent>, UserProducer>()

                 .AddSingleton(serviceProvider =>
                 {
                     var uri = new Uri("amqp://guest:guest@localhost:5672");
                     return new ConnectionFactory
                     {
                         Uri = uri
                     };
                 });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddControllers();

            services.AddAutoMapper(typeof(UserMapping));


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("UserRegistrationApi",
                                 new OpenApiInfo()
                                 {
                                     Title = "User Registration Api",
                                     Version = "1"
                                 });
                string[] name = Assembly.GetExecutingAssembly().GetName().Name.Split(".");

                var xmlCommentFile = $"{name[0]}.xml";
                var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                options.IncludeXmlComments(cmlCommentsFullPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/UserRegistrationApi/swagger.json", "User Registration Api");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
