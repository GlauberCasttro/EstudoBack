using DevIo.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIo.Data.Mappings
{
    //Configurando o banco de dados
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                 .IsRequired()
                 .HasColumnType("varchar(1000)");

            builder.Property(p => p.Imagem)
                 .IsRequired()
                 .HasColumnType("varchar(200)");

            builder.Property(p => p.Valor)
                 .IsRequired()
                 .HasColumnType("decimal(18,2)");

            builder.ToTable("Produtos");
        }
    }
}