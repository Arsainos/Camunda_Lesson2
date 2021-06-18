using Camunda.Worker;
using Camunda.Worker.Client;
using Camunda_Lesson2_Example3.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example3
{
    public class AnotherStartup
    {
        public IConfiguration Configuration;

        public AnotherStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Конфигурируем подключенеие к Camunda
            services.AddExternalTaskClient()
                .ConfigureHttpClient((provider, client) =>
                {
                    client.BaseAddress = new Uri("http://localhost:8080/engine-rest"); // Регестрируем подключение к Camunda Engine
                });

            // Конфигурируем обработчик
            services.AddCamundaWorker("ShootWorker")
                .AddHandler<ShootHandler>() // указываем Handler для обработки запроса
                .ConfigurePipeline(pipeline =>
                {
                    pipeline.Use(next => async context =>
                    {
                        var logger = context.ServiceProvider.GetRequiredService<ILogger<Startup>>(); // Использовать систему логирования 
                        logger.LogInformation("Начала обработки таска {id}", context.Task.Id); // Записать в лог информацию
                        await next(context); // Ожидание следующего запроса
                        logger.LogInformation("Начала обработки таска {id}", context.Task.Id); // Записать в лог информацию
                    });
                });

            services.AddHealthChecks(); // Добавить сервис работоспособности
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}
