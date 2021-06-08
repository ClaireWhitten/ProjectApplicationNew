using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectApplication.Classes
{
    public class ProjectApplicationContext : DbContext
    {

        public ProjectApplicationContext() : base("name=ProjectApplicationDBConnectString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProjectApplicationContext>());
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ProjectApplicationContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ProjectApplicationContext>());

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Student & StudentAddress entity
            modelBuilder.Entity<Employee>()
                        .HasOptional(e => e.UserAccount) // Mark userAccount property as optional in Employee entity
                        .WithRequired(ua => ua.Employee) // mark Employee property as required in StudentAddress entity. Cannot save useraccount without Employee
                        .WillCascadeOnDelete(true);
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<RegisteredProduct> RegisteredProducts { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public DbSet<PurchaseOrderProduct> PurchaseOrderProducts { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }

        public DbSet<SalesOrderProduct> SalesOrderProducts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<UserAccount> UserAccounts { get; set; }


       

    }
}
