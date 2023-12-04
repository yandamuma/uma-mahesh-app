using Microsoft.EntityFrameworkCore;
using UmaMahesh_BackApp.Models.Students;

namespace UmaMahesh_BackApp.Data;

public class MongoDBRepositoryContext : DbContext
{
    public MongoDBRepositoryContext(DbContextOptions<MongoDBRepositoryContext> options) : base(options)
    {

    }

    public DbSet<Students> students { get; set; }
}
