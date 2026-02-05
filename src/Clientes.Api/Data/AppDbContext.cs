using Clientes.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
}
