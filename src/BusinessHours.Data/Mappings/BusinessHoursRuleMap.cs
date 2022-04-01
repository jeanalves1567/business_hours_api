using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class BusinessHoursRuleMap : IEntityTypeConfiguration<BusinessHoursRule>
    {
        public void Configure(EntityTypeBuilder<BusinessHoursRule> builder)
        {
            builder.ToTable("Rules");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(60);
            builder.Property(r => r.Timezone).IsRequired().HasMaxLength(60);
            builder.Property(r => r.CreatedAt);
            builder.Property(r => r.UpdatedAt);
            builder.HasMany(r => r.WorkHours).WithOne(w => w.Rule).HasForeignKey(w => w.RuleId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
