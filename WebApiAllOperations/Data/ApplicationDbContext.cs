using Microsoft.EntityFrameworkCore;
using WebApiAllOperations.Model;

namespace WebApiAllOperations.Data;

public class ApplicationDbContext:DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
    {
        
    }

    public DbSet<Stock> Stock { get; set; }
    public DbSet<Comment> Comment { get; set; }
}