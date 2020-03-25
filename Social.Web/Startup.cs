using System.Collections.Generic;
using EventStore.ClientAPI;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Social.Application;
using Social.Application.Features.Members;
using Social.Infra;
using Social.Infra.EventStore;
using Social.Infra.Projections;
using Social.Infra.Queries;
using Social.Web.Hosted;

namespace Social.Web
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
            services.AddMediatR(typeof(IAggregateStore).Assembly);
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social API", Version = "v1" });
                c.CustomSchemaIds(t => t.FullName);
            });

            services.AddHostedService<HostedProjections>();

            //Projections
            services.AddSingleton<ProjectionsDispatcher>();
            services.AddScoped<IProjection, GetMemberProjection>();

            //infrastructure
            services.AddScoped(typeof(IAggregateStore), typeof(EventsAggregateStore));
            services.AddScoped<IEventStore, Infra.EventStore.EventStore>();

            services.Configure<EventStoreOptions>(Configuration.GetSection("EventStore"));
            services.AddSingleton(s =>
            {
                var options = s.GetRequiredService<IOptions<EventStoreOptions>>().Value;
                var connection = EventStoreConnection
                    .Create($"ConnectTo=tcp://{options.UserName}:{options.Password}@{options.Address}:{options.Port};");
                connection.ConnectAsync().Wait();
                return connection;
            });

            //Queries
            services.AddScoped(typeof(IQuery<Empty, IEnumerable<GetMembers.Result>>), typeof(GetMembersQuery));

            //Storing Data
            services.AddSingleton(new List<GetMembers.Result>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Social API V1");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSerilogRequestLogging();
        }
    }
}
