using MoqAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MoqAPI.Model;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    // Add DbSet properties for other model classes

}


