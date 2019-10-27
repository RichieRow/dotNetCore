using LZY.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.ViewModel
{
    public class StudentVM : IEntityVM
    {
        [Key]
        public Guid Id { get; set; }
        public string SName{get;set;}
        public DateTime EnrollmentTime { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public StudentVM()
        { }

        public StudentVM(Student bo)
        {
            Id = bo.Id;
            SName = bo.SName;
            EnrollmentTime = bo.EnrollmentTime;
            Enrollments = bo.Enrollments;
        }

    }
}
