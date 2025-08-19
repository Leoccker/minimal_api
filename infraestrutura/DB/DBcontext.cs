using Microsoft.EntityFrameworkCore;
using minimal_api.Dominio.Entidades;

namespace minimal_api.Infraestrutura.DB;

public class DBContext : DbContext
{

    private readonly IConfiguration _configuracaoAppSettings;
    public DBContext(IConfiguration configuracaoAppSettings)
    {

        _configuracaoAppSettings = configuracaoAppSettings;
    }

    public DbSet<ADM> ADMs { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ADM>().HasData(
            new ADM { Id = 1, Email = "adm@teste.com", Senha = "123456", Perfil = "Admin" }
        );
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuracaoAppSettings.GetConnectionString("mysql")?.ToString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
            }
        }

    }
}
