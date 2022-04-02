using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class WorkHoursMap : IEntityTypeConfiguration<WorkHours>
    {
        public void Configure(EntityTypeBuilder<WorkHours> builder)
        {
            builder.ToTable("work_hours");
            builder.HasKey(w => new { w.RuleId, w.Day });
            builder.Property(w => w.RuleId).HasColumnName("rule_id");
            builder.Property(w => w.Day).HasColumnName("day").HasConversion<string>();
            builder.Property(w => w.Open).HasColumnName("open").HasDefaultValue(false).IsRequired();
            builder.Property(w => w.Start).HasColumnName("start").HasMaxLength(5);
            builder.Property(w => w.Finish).HasColumnName("finish").HasMaxLength(5);
            builder.HasOne(w => w.Rule).WithMany(r => r.WorkHours).HasForeignKey(w => w.RuleId);
        }
    }
}
