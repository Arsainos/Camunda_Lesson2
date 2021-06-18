using Camunda.Worker;
using Camunda.Worker.Client;
using Camunda_Lesson2_Example3.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Camunda_Lesson2_Example3
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ������������� ������������ � Camunda
            services.AddExternalTaskClient()
                .ConfigureHttpClient((provider, client) =>
                {
                    client.BaseAddress = new Uri("http://localhost:8080/engine-rest"); // ������������ ����������� � Camunda Engine
                });

            // ������������� ����������
            services.AddCamundaWorker("StrafeWorker")
                .AddHandler<StrafeHandler>() // ��������� Handler ��� ��������� �������
                .ConfigurePipeline(pipeline =>
                {
                    pipeline.Use(next => async context =>
                    {
                        var logger = context.ServiceProvider.GetRequiredService<ILogger<Startup>>(); // ������������ ������� ����������� 
                        logger.LogInformation("������ ��������� ����� {id}", context.Task.Id); // �������� � ��� ����������
                        await next(context); // �������� ���������� �������
                        logger.LogInformation("������ ��������� ����� {id}", context.Task.Id); // �������� � ��� ����������
                    });
                });

            services.AddHealthChecks(); // �������� ������ �����������������
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
        }
    }
}
