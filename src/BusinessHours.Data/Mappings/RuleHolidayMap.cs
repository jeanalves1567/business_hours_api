using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class RuleHolidayMap : IEntityTypeConfiguration<RuleHoliday>
    {
        public void Configure(EntityTypeBuilder<RuleHoliday> builder)
        {
            builder.ToTable("rules_holidays");
            builder.HasKey(rh => new { rh.RuleId, rh.HolidayId });
            builder.Property(rh => rh.RuleId).HasColumnName("rule_id");
            builder.Property(rh => rh.HolidayId).HasColumnName("holiday_id");
            builder.HasOne(rh => rh.Rule).WithMany(r => r.Holidays).HasForeignKey(rh => rh.RuleId);
            builder.HasOne(rh => rh.Holiday).WithMany(h => h.Rules).HasForeignKey(rh => rh.HolidayId);
        }
    }
}
