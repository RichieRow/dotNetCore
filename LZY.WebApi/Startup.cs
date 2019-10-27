using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.DataAccess;
using LZY.DataAccess.EntityFramework;
using LZY.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LZY.WebApi
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
            // 添加 EF Core 框架，连接串在appsettings设置
            services.AddDbContext<SchoolDbContext>(d => d.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            #region 域控制器相关的依赖注入服务清单
            services.AddTransient<IEntityRepository<Student>, EntityRepository<Student>>();
            services.AddTransient<IEntityRepository<Course>, EntityRepository<Course>>();
            services.AddTransient<IEntityRepository<Enrollment>, EntityRepository<Enrollment>>();
            #endregion
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //跨域
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });


           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("any");
            app.UseHttpsRedirection();//跨域
            app.UseMvc();
        }
    }
}
