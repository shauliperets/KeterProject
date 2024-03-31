using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        /*public DataContext()
        {
               
        }*/

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<SubDepartment>? SubDepartments { get; set; }
        public DbSet<Log>? Logs { get; set; }

        //add migration for log and test user
    }
}