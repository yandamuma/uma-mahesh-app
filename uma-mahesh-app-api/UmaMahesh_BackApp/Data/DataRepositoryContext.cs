using Microsoft.EntityFrameworkCore;
using UmaMahesh_BackApp.Models.Product;
using UmaMahesh_BackApp.Models.Products;
using UmaMahesh_BackApp.Models.Restaurants;
using UmaMahesh_BackApp.Models.User;

namespace UmaMahesh_BackApp.Data
{
    public class DataRepositoryContext : DbContext
    {
        public DataRepositoryContext(DbContextOptions<DataRepositoryContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<UserRegistration> UserRegistration {  get; set; }



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

            //    modelBuilder.Entity<Products>().Ignore(t => t.Id);
            //    base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Products>()
            //                        .Property(p => p.Id)
            //                        .ValueGeneratedNever();
        //}

}
}
