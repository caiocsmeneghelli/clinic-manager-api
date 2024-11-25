using ClinicManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(reg => reg.Id);

            builder.HasOne(reg => reg.User)
                .WithMany()
                .HasForeignKey(reg => reg.IdUser)
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(reg => reg.Address, addr =>
            {
                addr.Property(a => a.Street).HasColumnName("Street");
                addr.Property(a => a.City).HasColumnName("City");
                addr.Property(a => a.UF).HasColumnName("UF");
                addr.Property(a => a.Country).HasColumnName("Country");
            });



            builder.OwnsOne(d => d.PersonDetail, pd =>
            {
                pd.Property(p => p.FirstName).HasColumnName("FirstName");
                pd.Property(p => p.LastName).HasColumnName("LastName");
                pd.Property(p => p.BirthDate).HasColumnName("BirthDate");
                pd.Property(p => p.PhoneNumber).HasColumnName("PhoneNumber");
                pd.Property(p => p.Email).HasColumnName("Email");
                pd.Property(p => p.CPF).HasColumnName("CPF");
                pd.Property(p => p.BloodType).HasColumnName("BloodType");
            });
        }
    }
}
