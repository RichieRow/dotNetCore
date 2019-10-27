using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using LZY.Model.enumsType;

namespace LZY.Model
{
    public class Course : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
       
        public IComparable<Enrollment> Enrollments { get; set; }
        public Course()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
