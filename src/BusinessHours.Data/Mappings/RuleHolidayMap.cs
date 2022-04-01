using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class RuleHolidayMap : IEntityTypeConfiguration<RuleHoliday>
    {
        public void Configure(EntityTypeBuilder<RuleHoliday> builder)
        {
            builder.ToTable("RulesHolidays");
            builder.HasKey(rh => new { rh.RuleId, rh.HolidayId });
            builder.HasOne(rh => rh.Rule).WithMany(r => r.Holidays).HasForeignKey(rh => rh.RuleId);
            builder.HasOne(rh => rh.Holiday).WithMany(h => h.Rules).HasForeignKey(rh => rh.HolidayId);
        }
    }
}
