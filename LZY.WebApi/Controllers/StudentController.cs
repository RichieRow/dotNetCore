using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LZY.DataAccess;
using LZY.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LZY.WebApi.Controllers
{
 

    public class StudentController : BaseController
    {
        private readonly IEntityRepository<Student> _Student;
        public StudentController(IEntityRepository<Student> student)
        {
            _Student = student;
        }
        [HttpPost]
        public IQueryable Get()
        {
            var list = _Student.GetAllIncluding(x=>x.Enrollments);
            return list;
        }

        [HttpPost]
        public IQueryable GetList()
        {
            var list = _Student.GetAllIncluding(x => x.Enrollments);
            return list;
        }


    }
}