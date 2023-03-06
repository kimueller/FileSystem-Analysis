using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAP_RekursivDateisystem.DAL.Models
{
    public class File
    {
        [Key]
        public int FileID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public float Size { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [ForeignKey("Directory")]
        public string DirectoryID { get; set; }
        public Directory Directory { get; set; }
        
    }
}
