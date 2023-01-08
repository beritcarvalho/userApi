using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "AccessControl")
                .HasComment("Tabela de Perfis de usuários");

            #region PrimaryKey
            builder
                .HasKey(user => user.Id)
                .HasName("User_PK");

            builder
                .Property(user => user.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnName("User_Id")
                .HasComment("Chave Primária da tabela User");
            #endregion

            #region Constrainsts
            builder.Property(user => user.Login)
                .IsRequired() 
                .HasColumnName("Login") 
                .HasColumnType("NVARCHAR") 
                .HasMaxLength(30)
                .HasComment("Login do Usuario");

            builder.Property(user => user.Password_Hash)
                .IsRequired()
                .HasColumnName("Password_Hash")
                .HasColumnType("VARCHAR")
                .HasMaxLength(255)
                .HasComment("Senha criptografada");

            builder.Property(user => user.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Data de criação do usuário");


            builder.Property(user => user.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT")
                .HasComment("Usuário Ativo");

            builder.Property(user => user.Active_Date)                
                .HasColumnName("Active_Date")
                .HasColumnType("DATETIME")
                .HasComment("Data de Ativação do usuário");

            builder.Property(user => user.Inactive_Date)
                .HasColumnName("Inactive_Date")
                .HasColumnType("DATETIME")
                .HasComment("Data de Desativação do usuário");

            builder.Property(user => user.Last_Update_Date)
                .IsRequired()
                .HasColumnName("Last_Update_Date")
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETDATE()")
                .HasComment("Ultima atualização do usuário");

            builder.Property(user => user.Acco_Id)
                .IsRequired()
                .HasColumnName("Acco_Id")
                .HasColumnType("INT")
                .HasComment("FK(Chave Primária da Tabela Account)");
            
            builder.Property(user => user.Role_Id)                
                .HasColumnName("Role_Id")
                .HasColumnType("TINYINT")
                .HasComment("FK(Chave Primária da Tabela Role)");
            #endregion

            #region Indexes

            builder.HasIndex(x => x.Login, "IX_User_Login")
                .IsUnique();

            builder.HasIndex(x => x.Acco_Id, "IX_User_AccountId")
                .IsUnique();
            #endregion

            #region Relationships
            builder
                .HasOne(user => user.Account)
                .WithOne(account => account.User)
                .HasForeignKey<User>(user => user.Acco_Id)
                .HasConstraintName("FK_Account");


            builder
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(user => user.Role_Id)               
                .HasConstraintName("FK_Role")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region PopulationData
            builder.HasData(
                  new User
                  {
                      Id = 1,
                      Login = "admin",
                      Password_Hash = "10000.U4nuXF9Jhi87x0jZi6893A==.rSI4G0QL9aA5kByIf5yz+BtRKW4OMEFlzi+VCjyWFl0=",
                      Active = true,
                      Acco_Id = 1,
                      Role_Id = 1,
                  },
                 new User
                 {
                      Id = 2,
                      Login = "sindico",
                      Password_Hash = "10000.Tj/1xyXdwY3YYLi3nkKIUQ==.naJ9GBVRb/0Mc8MBupXPQiJFEeAAokKMTXz/pxLHUP4=",
                      Active = true,
                      Acco_Id = 2,
                      Role_Id = 2,
                 },
                new User
                {
                    Id = 3,
                    Login = "porteiro",
                    Password_Hash = "10000.fpxC2PTBMdH7WsyRPDKrPg==.kQpbl9i0XiaCG775bU2CJKvLrJ6mCEsrhhlr91RdTeE=",
                    Active = true,
                    Acco_Id = 3,
                    Role_Id = 4,
                });
            #endregion
        }
    }
}
