using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.Model
{
    public class Student : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string SName { get; set; }
        public DateTime EnrollmentTime { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }

        public Student()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
