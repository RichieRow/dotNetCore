using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LZY.DataAccess.EntityFramework;
using LZY.DataAccess.EntityFramework.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LZY.WebApi
{
    public class Program
    { /// <summary>
      /// 依赖注入
      /// </summary>
      /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SchoolDbContext>();
                    DbInitializer.Intializer(context);
                }
                catch (Exception e)
                {

                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "初始化失败");
                }

            }

            host.Run();
            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().Build();
        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
