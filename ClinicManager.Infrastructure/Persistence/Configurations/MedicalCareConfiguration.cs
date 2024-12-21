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
    public class MedicalCareConfiguration : IEntityTypeConfiguration<MedicalCare>
    {
        public void Configure(EntityTypeBuilder<MedicalCare> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasOne(reg => reg.Doctor)
                .WithMany(r => r.MedicalCares)
                .HasForeignKey(reg => reg.IdDoctor);

            builder.HasOne(reg => reg.Patient)
                .WithMany(p => p.MedicalCares)
                .HasForeignKey(m => m.IdPatient);

            builder.HasOne(reg => reg.Service)
                .WithOne(s => s.MedicalCare)
                .HasForeignKey<MedicalCare>(m => m.IdService)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
