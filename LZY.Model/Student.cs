using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.Model
{
    public class Student : IEntityBase
    {
        [Key]
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SName { get; set; }
        public DateTime EnrollmentTime { get; set; }
        public IComparable<Enrollment> Enrollments { get; set; }

        public Student()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
