using AssignDAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignDAL.Contexts
{
    public class MVCDBContext : IdentityDbContext<ApplicationUser>
    {
        public MVCDBContext(DbContextOptions<MVCDBContext> options):base(options)
        {

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlServer("Server = .;Database = MVCApp; Trusted_Connection = true; MultipleActiveResultSets = true");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
      


    }
}
