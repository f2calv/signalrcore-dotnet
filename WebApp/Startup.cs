using CasCap.Hubs;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
namespace CasCap
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            /*
            // set extensions to default resolver.
            var resolver = MessagePack.Resolvers.CompositeResolver.Create(
                // enable extension packages first
                DynamicEnumAsStringResolver.Instance,
                // finaly use standard(default) resolver
                StandardResolver.Instance
            );
            var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);

            // pass options to every time or set as default
            MessagePackSerializer.DefaultOptions = options;
            */
            services.AddSignalR(hubOptions =>
                {
                    //hubOptions.EnableDetailedErrors = true;
                    //hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(1);
                })
                .AddMessagePackProtocol()
                //.AddMessagePackProtocol(options =>
                //{
                //    options.FormatterResolvers = new List<IFormatterResolver>()
                //    {
                //        MessagePack.Resolvers.StandardResolver.Instance,
                //        MessagePack.Resolvers.DynamicEnumAsStringResolver.Instance
                //    };
                //})
                ;
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MyHub>("/myhub", options=>
                {
                });
                //endpoints.MapHub<MyHub>("/myhub", options =>
                //{
                //    options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
                //});
                endpoints.MapRazorPages();
            });
        }
    }
}