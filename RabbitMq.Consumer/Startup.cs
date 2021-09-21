using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMq.Consumer.Commands;
using RabbitMq.Consumer.Commands.Handlers;
using RabbitMQ.Client; 

namespace RabbitMq.Consumer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddMediatR(Assembly.GetExecutingAssembly())
               .AddTransient<IRequestHandler<LogCommand, Unit>, LogCommandHandler>()
               .AddHostedService<LogConsumer>()
               .AddSingleton(serviceProvider =>
               {
                   var uri = new Uri("amqp://guest:guest@localhost:5672");
                   return new ConnectionFactory
                   {
                       Uri = uri,
                       DispatchConsumersAsync = true
                   };
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            
        }
    }
}
