using Microsoft.EntityFrameworkCore;
using Token_API.Models;

namespace Token_API.Data;

public class Context : DbContext{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Token> Tokens { get; set; }
}