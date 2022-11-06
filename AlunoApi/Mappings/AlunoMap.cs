using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AlunoMap : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.HasData(
            new Aluno
            {
                Id = 1,
                Nome = "Rogerio",
                Email = "rogerio@gmail.com",
                Idade = 30
            }
        );

    }
}
