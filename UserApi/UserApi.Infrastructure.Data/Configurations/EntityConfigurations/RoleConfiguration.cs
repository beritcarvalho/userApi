using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role", "AccessControl")
                .HasComment("Tabela de tipos de login");

            #region PrimaryKey
            builder
                .HasKey(role => role.Id)
                .HasName("Role_PK");

            builder
                .Property(role => role.Id)
                .HasColumnType("TINYINT")
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnName("Role_Id")
                .HasComment("Chave Primária da tabela Role");
            #endregion

            #region Constrainsts
            builder.Property(role => role.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR(20)")
                .HasMaxLength(20)
                .HasComment("Nome da Função do Usuário");
            #endregion

            #region Indexes
            builder.HasIndex(x => x.Name, "IX_Role_Name")
                .IsUnique();
            #endregion

            # region PopulationData
            builder.HasData(
                new Role
                {
                    Id = 1,
                    Name = "admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "manager"
                },
                new Role
                {
                    Id = 3,
                    Name = "sub-Manager"
                },
                new Role
                {
                    Id = 4,
                    Name = "doorman"
                },
                new Role
                {
                    Id = 5,
                    Name = "resident"
                });
            #endregion
        }
    }
}
