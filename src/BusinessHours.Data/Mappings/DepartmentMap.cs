using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(60);
            builder.Property(d => d.Type).IsRequired().HasMaxLength(30);
            builder.HasIndex(d => d.ExternalId).IsUnique();
            builder.HasOne(d => d.Rule).WithMany(r => r.Departments).HasForeignKey(d => d.RuleId);
        }
    }
}
