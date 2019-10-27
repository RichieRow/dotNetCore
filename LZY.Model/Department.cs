using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LZY.Model
{
   public class Department : IEntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
