using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebsiteWebApi.Repositories;

namespace WebsiteWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<WebSiteContext>();
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<WebSiteContext>();
            context.Database.EnsureCreated();
            if (!context.SocialTypes.Any())
            {
                context.SocialTypes.AddRange(
                new Models.SocialType { Id = 1, Title = "Facebook", CSS = "fa-facebook" },
                new Models.SocialType { Id = 2, Title = "Twitter", CSS = "twitter" },
                new Models.SocialType { Id = 3, Title = "Github", CSS = "fa-github" },
                new Models.SocialType { Id = 4, Title = "GooglePlus", CSS = "fa-google-plus" },
                new Models.SocialType { Id = 5, Title = "LinkedIn", CSS = "fa-linkedin" }
            );

                //context.Posts.AddRange(
                //    new Post { PostId = 1, Title = "Title 1", Content = "Content 1", BlogId = 1 },
                //    new Post { PostId = 2, Title = "Title 2", Content = "Content 2", BlogId = 2 },
                //    new Post { PostId = 3, Title = "Title 3", Content = "Content 3", BlogId = 3 }
                //    );
                context.SaveChanges();
            }
        }
    }
}
