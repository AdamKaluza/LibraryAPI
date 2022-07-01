using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Context;

public class MainContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Author> Author { get; set; }
    public DbSet<Book> Book { get; set; }
    public DbSet<BookCategory> BookCategory { get; set; }
    public DbSet<Address> Address { get; set; }
    
    public MainContext()
    {
    }
    
    public MainContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=dbo.Library.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .OnDelete(DeleteBehavior.Cascade);
    }
}