using LZY.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace LZY.ViewModel
{
   public class CourseVM:IEntityVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public CourseVM()
        { }

        public CourseVM(Course bo)
        {
            Id = bo.Id;
            Title = bo.Title;
            Credits = bo.Credits;
            Enrollments = bo.Enrollments;
        }
    }
}
