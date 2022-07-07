using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student: Record
    {
        [MaxLength(150)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [MaxLength(150)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual Group? Group { get; set; }
        
    }
}
