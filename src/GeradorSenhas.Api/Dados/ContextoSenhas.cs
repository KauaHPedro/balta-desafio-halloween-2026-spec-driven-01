using Microsoft.EntityFrameworkCore;

namespace GeradorSenhas.Api.Dados;

public sealed class ContextoSenhas : DbContext
{
    public ContextoSenhas(DbContextOptions<ContextoSenhas> opcoes) : base(opcoes)
    {
    }

    public DbSet<SenhaArmazenada> Senhas => Set<SenhaArmazenada>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SenhaArmazenada>(entidade =>
        {
            entidade.ToTable("Senhas");
            entidade.HasKey(senha => senha.Id);
            entidade.Property(senha => senha.Senha).IsRequired();
        });
    }
}
