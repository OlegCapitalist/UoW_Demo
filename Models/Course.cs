#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Course: Record
    {
        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [MaxLength(150)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public virtual List<Group> Groups { get; set; }
    }
}
