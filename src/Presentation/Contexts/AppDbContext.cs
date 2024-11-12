using Microsoft.EntityFrameworkCore;
using Presentation.Models;

namespace Presentation.Context;
public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Movie> Movies { get; set; }
}