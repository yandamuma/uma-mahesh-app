using Microsoft.EntityFrameworkCore;
using UmaMahesh_BackApp.Entities.Products;
using UmaMahesh_BackApp.Entities.Restaurants;
using UmaMahesh_BackApp.Entities.Users;


namespace UmaMahesh_BackApp.Data
{
    public class DataRepositoryContext : DbContext
    {
        public DataRepositoryContext(DbContextOptions<DataRepositoryContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
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
