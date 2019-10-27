using LZY.Model;
using LZY.Model.enumsType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LZY.DataAccess.EntityFramework.Data
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public class DbInitializer
    {
        static SchoolDbContext _Context;
        static Student[] students = new Student[]
                {
                new Student{ SName="XiaoMing",EnrollmentTime=DateTime.Parse("2018-02-01")},
                new Student{ SName="XiaoMin",EnrollmentTime=DateTime.Parse("2018-02-03")},
                new Student{ SName="XiaoMi",EnrollmentTime=DateTime.Parse("2018-02-04")},
                new Student{ SName="XiaoM",EnrollmentTime=DateTime.Parse("2018-02-05")},
                new Student{ SName="Xiao",EnrollmentTime=DateTime.Parse("2018-02-06")}
                };
        static Course[] course = new Course[]
                {
                new Course{ Title="Chinese",Credits=10},
                new Course{ Title="Biological",Credits=2},
                new Course{ Title="Physical",Credits=7},
                new Course{ Title="Math",Credits=10}
                };
        public static void Intializer(SchoolDbContext context)
        {
            _Context = context;
            context.Database.EnsureCreated();//创建数据库，如果创建了，则不会重新创建


            _AddStudent();//添加学生
            _AddCourse();//添加科目
            _AddEnrollment();//登记成绩
        }

        private static void _AddStudent()
        {
            if (_Context.Students.Any())//有数据则不执行
            {
                return;
            }

            foreach (Student s in students)
            {
                _Context.Students.Add(s);
                _Context.SaveChanges();
            }
        }

        public static void _AddCourse()
        {
            if (_Context.Courses.Any())
            {
                return;
            }
            foreach (Course c in course)
            {
                _Context.Courses.Add(c);
                _Context.SaveChanges();
            }
        }
        public static void _AddEnrollment()
        {
            if (_Context.Courses.Any())
            {
                return;
            }
            var enrollment = new Enrollment[]
                {
                new Enrollment{ StudentId=students[0].Id,CourseId=course[0].Id,Grade=CourseGrade.A},
                new Enrollment{ StudentId=students[0].Id,CourseId=course[1].Id,Grade=CourseGrade.C},
                new Enrollment{ StudentId=students[0].Id,CourseId=course[2].Id,Grade=CourseGrade.D},
                new Enrollment{ StudentId=students[0].Id,CourseId=course[3].Id,Grade=CourseGrade.D},

                new Enrollment{ StudentId=students[1].Id,CourseId=course[0].Id,Grade=CourseGrade.D},
                new Enrollment{ StudentId=students[1].Id,CourseId=course[1].Id,Grade=CourseGrade.C},
                new Enrollment{ StudentId=students[1].Id,CourseId=course[2].Id,Grade=CourseGrade.D},
                new Enrollment{ StudentId=students[1].Id,CourseId=course[3].Id,Grade=CourseGrade.D},

                new Enrollment{ StudentId=students[2].Id,CourseId=course[0].Id,Grade=CourseGrade.A},
                new Enrollment{ StudentId=students[2].Id,CourseId=course[1].Id,Grade=CourseGrade.C},
                new Enrollment{ StudentId=students[2].Id,CourseId=course[2].Id,Grade=CourseGrade.B},
                new Enrollment{ StudentId=students[2].Id,CourseId=course[3].Id,Grade=CourseGrade.D},

                new Enrollment{ StudentId=students[3].Id,CourseId=course[0].Id,Grade=CourseGrade.A},
                new Enrollment{ StudentId=students[3].Id,CourseId=course[1].Id,Grade=CourseGrade.C},
                new Enrollment{ StudentId=students[3].Id,CourseId=course[2].Id,Grade=CourseGrade.A},
                new Enrollment{ StudentId=students[3].Id,CourseId=course[3].Id,Grade=CourseGrade.D},

                new Enrollment{ StudentId=students[4].Id,CourseId=course[0].Id,Grade=CourseGrade.A},
                new Enrollment{ StudentId=students[4].Id,CourseId=course[1].Id,Grade=CourseGrade.E},
                new Enrollment{ StudentId=students[4].Id,CourseId=course[2].Id,Grade=CourseGrade.D},
                new Enrollment{ StudentId=students[4].Id,CourseId=course[3].Id,Grade=CourseGrade.D},
                };
            foreach (Enrollment e in enrollment)
            {
                _Context.Enrollments.Add(e);
                _Context.SaveChanges();
            }
        }


    }
}
