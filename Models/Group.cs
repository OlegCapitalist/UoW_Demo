

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;


namespace Models
{
    public class Group: Record
    {
        public Group()
        {
            Students = new List<Student>();
        }

        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public virtual List<Student> Students { get; set; }

    }
}
