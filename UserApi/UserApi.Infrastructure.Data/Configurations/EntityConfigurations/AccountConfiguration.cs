using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Account", "AccessControl")
                .HasComment("Tabela de Pessoas Cadastradas");

            #region PrimaryKey
            builder
                .HasKey(account => account.Id)
                .HasName("Account_PK");

            builder
                .Property(account => account.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnName("Acco_Id")
                .HasComment("Chave Primária da tabela Account");
            #endregion

            #region Constrainsts
            builder.Property(account => account.First_Name)
                .IsRequired() 
                .HasColumnName("First_Name") 
                .HasColumnType("NVARCHAR") 
                .HasMaxLength(30)
                .HasComment("Nome");

            builder.Property(account => account.Last_Name)
                .IsRequired()
                .HasColumnName("Last_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30)
                .HasComment("Sobrenome");

            builder.Property(account => account.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11)
                .HasComment("CPF");

            builder.Property(account => account.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11)
                .HasComment("Telefone para contato");

            builder.Property(account => account.Email)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .HasComment("Email");

            builder.Property(account => account.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data de Criação do perfil");

            builder.Property(account => account.Last_Update_Date)
                .IsRequired()
                .HasColumnName("Last_Update_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Ultima atualização do cadastro");
            #endregion

            #region Indexes

            builder.HasIndex(x => x.Email, "IX_Account_Email")
                .IsUnique(); 

            builder.HasIndex(x => x.Cpf, "IX_Account_Cpf") 
                .IsUnique();

            #endregion

            # region PopulationData
            builder.HasData(
                new Account
                {
                    Id = 1,
                    First_Name = "Admin",
                    Last_Name = "System",
                    Cpf = "0124567890",
                    Phone = "11987654321",
                    Email = "admin@admin.com"
                });
            #endregion
        }
    }
}
