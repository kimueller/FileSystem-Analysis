using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAP_RekursivDateisystem.DAL
{
    internal class FileDTO
    {
        public string Name { get; set; }
        
        public DateTime CreationDate { get; set; }

        public int Size { get; set; }
    }
}
