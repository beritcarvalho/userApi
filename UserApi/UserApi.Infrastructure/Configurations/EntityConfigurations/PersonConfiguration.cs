using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Domain.Entities;

namespace UserApi.Infrastructure.Data.Configurations.EntityConfigurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person", "AccessControl");

            #region PrimaryKey
            builder
                .HasKey(person => person.Id)
                .HasName("Person_PK");

            builder
                .Property(person => person.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .HasColumnName("Pers_Id");
            #endregion

            #region Constrainsts
            builder.Property(person => person.First_Name)
                .IsRequired() 
                .HasColumnName("First_Name") 
                .HasColumnType("NVARCHAR") 
                .HasMaxLength(30);

            builder.Property(person => person.Last_Name)
                .IsRequired()
                .HasColumnName("Last_Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(30);

            builder.Property(person => person.Cpf)
                .IsRequired()
                .HasColumnName("Cpf")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(person => person.Phone)
                .IsRequired()
                .HasColumnName("Phone")
                .HasColumnType("VARCHAR")
                .HasMaxLength(11);

            builder.Property(person => person.Email)
                .HasColumnName("Email")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            builder.Property(person => person.Create_Date)
                .IsRequired()
                .HasColumnName("Create_Date")
                .HasColumnType("DATETIME");

            builder.Property(person => person.Active)
                .IsRequired()
                .HasColumnName("Active")
                .HasColumnType("BIT");

            builder.Property(person => person.Inactive_Date)
                .HasColumnName("Inactive_Date")
                .HasColumnType("DATETIME");

            builder.Property(person => person.Activation_Date)
                .HasColumnName("Activation_Date")
                .HasColumnType("DATETIME");

            builder.Property(person => person.Change_Date)
                .HasColumnName("Change_Date")
                .HasColumnType("DATETIME");
            #endregion

            #region Indexes

            builder.HasIndex(x => x.Email, "IX_Person_Email")
                .IsUnique(); 

            builder.HasIndex(x => x.Cpf, "IX_Person_Cpf") 
                .IsUnique();

            #endregion
        }
    }
}
