using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class HolidayMap : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("Holidays");
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Name).IsRequired().HasMaxLength(50);
            builder.Property(h => h.Month).IsRequired();
            builder.Property(h => h.Day).IsRequired();
            builder.Property(h => h.AllDay).IsRequired().HasDefaultValue(true);
            builder.HasMany(h => h.Rules).WithOne(rh => rh.Holiday).HasForeignKey(rh => rh.HolidayId);
        }
    }
}
