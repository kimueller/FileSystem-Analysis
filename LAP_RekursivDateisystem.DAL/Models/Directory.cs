using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAP_RekursivDateisystem.DAL.Models
{
    public class Directory
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int Size { get; set; }

        public virtual ICollection<File> Files { get;}
    }
}
