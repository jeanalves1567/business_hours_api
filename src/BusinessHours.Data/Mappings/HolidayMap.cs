using BusinessHours.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessHours.Data.Mappings
{
    public class HolidayMap : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            builder.ToTable("holidays");
            builder.HasKey(h => h.Id);
            builder.Property(h => h.Id).HasColumnName("id");
            builder.Property(h => h.CreatedAt).HasColumnName("created_at");
            builder.Property(h => h.UpdatedAt).HasColumnName("updated_at");
            builder.Property(h => h.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            builder.Property(h => h.Month).HasColumnName("month").IsRequired();
            builder.Property(h => h.Day).HasColumnName("day").IsRequired();
            builder.Property(h => h.AllDay).HasColumnName("all_day").IsRequired().HasDefaultValue(true);
            builder.Property(h => h.Year).HasColumnName("year");
            builder.Property(h => h.Start).HasColumnName("start");
            builder.Property(h => h.Finish).HasColumnName("finish");
            builder.HasMany(h => h.Rules).WithOne(rh => rh.Holiday).HasForeignKey(rh => rh.HolidayId);
        }
    }
}
