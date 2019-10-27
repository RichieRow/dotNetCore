using LZY.Model;
using LZY.Model.enumsType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.ViewModel
{
    public class EnrollmentVM
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public CourseGrade Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public EnrollmentVM()
        { }

        public EnrollmentVM(Enrollment bo)
        {
            Id = bo.Id;
            StudentId = bo.StudentId;
            CourseId = bo.CourseId;
            Grade = bo.Grade;
            if (bo.Student != null)
            {
                Student = bo.Student;
            }
            if (bo.Course != null)
            {
                Course = bo.Course;
            }
        }
    }
}
