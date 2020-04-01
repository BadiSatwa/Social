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
using Social.Application.Features.FriendshipInvitations;
using Social.Application.Features.Members;
using Social.Application.Features.Posts;
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social API", Version = "v1" });
                c.CustomSchemaIds(t => t.FullName);
            });

            services.AddHostedService<StartupActions>();

            //Projections
            services.AddSingleton<IProjectionsManager, EventStoreProjectionsManager>();
            services.AddSingleton<IProjectionDefinitionsProvider, EmbeddedProjectionDefinitionsProvider>();

            //infrastructure
            services.AddScoped(typeof(IAggregateStore), typeof(EventsAggregateStore));
            services.AddScoped<IEventStore, Infra.EventStore.EventStore>();

            services.Configure<EventStoreOptions>(Configuration.GetSection("EventStore"));
            services.AddSingleton(s =>
            {
                var options = s.GetRequiredService<IOptions<EventStoreOptions>>().Value;
                var connection = EventStoreConnection
                    .Create($"ConnectTo=tcp://{options.UserName}:{options.Password}@{options.Address}:{options.Port};");
                return connection;
            });

            //Queries
            services.AddScoped(typeof(IQuery<Empty, IEnumerable<GetMembers.Result>>), typeof(GetMembersQuery));
            services.AddScoped(typeof(IQuery<Empty, IEnumerable<GetFriends.Result>>), typeof(GetFriendsQuery));
            services.AddScoped(typeof(IQuery<Empty, IEnumerable<GetSentInvitations.Result>>), typeof(GetSendInvitationsQuery));
            services.AddScoped(typeof(IQuery<Empty, IEnumerable<GetPosts.Result>>), typeof(GetPostsQuery));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

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
