using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class WorkHoursMap : IEntityTypeConfiguration<WorkHours>
    {
        public void Configure(EntityTypeBuilder<WorkHours> builder)
        {
            builder.ToTable("WorkHours");
            builder.HasKey(w => new { w.RuleId, w.Day });
            builder.Property(w => w.Day).HasConversion<string>();
            builder.Property(w => w.Open).HasDefaultValue(false).IsRequired();
            builder.Property(w => w.Start).HasMaxLength(5);
            builder.Property(w => w.Finish).HasMaxLength(5);
            builder.HasOne(w => w.Rule).WithMany(r => r.WorkHours).HasForeignKey(w => w.RuleId);
        }
    }
}
