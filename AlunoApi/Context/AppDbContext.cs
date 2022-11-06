using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // linha de código necessária para a autenticação, futuramente
        modelBuilder.ApplyConfiguration(new AlunoMap());
        //modelBuilder.Entity<Aluno>().HasData(
        //    new Aluno
        //    {
        //        Id = 1,
        //        Nome = "Rogerio",
        //        Email = "rogerio@gmail.com",
        //        Idade = 30
        //    }
        //);
    }
    public DbSet<Aluno> Alunos { get; set; }
}
