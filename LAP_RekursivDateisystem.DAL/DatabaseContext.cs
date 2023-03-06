using LAP_RekursivDateisystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = LAP_RekursivDateisystem.DAL.Models.File;
using Directory = LAP_RekursivDateisystem.DAL.Models.Directory;

namespace LAP_RekursivDateisystem.DAL
{

    public class DatabaseContext : DbContext
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Files;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        public virtual DbSet<Directory> Directories { get; set; }
        public virtual DbSet<File> Files { get; }
    }
}
