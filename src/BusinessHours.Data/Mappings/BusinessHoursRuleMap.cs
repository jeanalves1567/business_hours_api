using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class BusinessHoursRuleMap : IEntityTypeConfiguration<BusinessHoursRule>
    {
        public void Configure(EntityTypeBuilder<BusinessHoursRule> builder)
        {
            builder.ToTable("rules");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).HasColumnName("id");
            builder.Property(r => r.Name).HasColumnName("name").IsRequired().HasMaxLength(60);
            builder.Property(r => r.Timezone).HasColumnName("timezone").IsRequired().HasMaxLength(60);
            builder.Property(r => r.CreatedAt).HasColumnName("created_at");
            builder.Property(r => r.UpdatedAt).HasColumnName("updated_at");
            builder.HasMany(r => r.WorkHours).WithOne(w => w.Rule).HasForeignKey(w => w.RuleId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(r => r.Departments).WithOne(d => d.Rule).HasForeignKey(d => d.RuleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(r => r.Holidays).WithOne(rh => rh.Rule).HasForeignKey(rh => rh.RuleId);
        }
    }
}
