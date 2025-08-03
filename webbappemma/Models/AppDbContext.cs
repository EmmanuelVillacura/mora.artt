using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using webbappemma.Models;

public class AppDbContext : DbContext

{
    public DbSet<Usuario> TblUsuario { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=feer;Integrated Security=True;");
        }
    }
}
