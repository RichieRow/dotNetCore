using LZY.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.DataAccess.EntityFramework
{
   public class SchoolDbContext:DbContext
    {
        /// <summary>
        /// 将SchoolDbContext与数据上下文联通
        /// </summary>
        /// <param name="options"></param>
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            
        }


        /// <summary>
        /// 生成数据库表 名称为复数
        /// </summary>
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<Department> Departments { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// 如果不需要 DbSet<T> 所定义的属性名称作为数据库表的名称，可以在下面的位置自己重新定义
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Student>().ToTable("Student"); //设置生成对应数据库表的名称
            base.OnModelCreating(modelBuilder);

        }

    }
}
