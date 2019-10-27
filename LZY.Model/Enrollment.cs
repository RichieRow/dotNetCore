using LZY.Model.enumsType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.Model
{
   public class Enrollment : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public CourseGrade Grade { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public Enrollment() => this.Id = Guid.NewGuid();
    }
}
