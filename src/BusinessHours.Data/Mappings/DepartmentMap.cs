using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class DepartmentMap : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id");
            builder.Property(d => d.CreatedAt).HasColumnName("created_at");
            builder.Property(d => d.UpdatedAt).HasColumnName("updated_at");
            builder.Property(d => d.Name).HasColumnName("name").IsRequired().HasMaxLength(60);
            builder.Property(d => d.Type).HasColumnName("type").IsRequired().HasMaxLength(30);
            builder.Property(d => d.RuleId).HasColumnName("rule_id");
            builder.Property(d => d.ExternalId).HasColumnName("external_id").IsRequired();
            builder.HasIndex(d => d.ExternalId).IsUnique();
            builder.HasOne(d => d.Rule).WithMany(r => r.Departments).HasForeignKey(d => d.RuleId);
        }
    }
}
