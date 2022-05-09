using CitaVehiculosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CitaVehiculosApi.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options)    {    }

    public DbSet<Cita> Citas { get; set; }
}
