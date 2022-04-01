using System;
using System.Text.RegularExpressions;
using BusinessHours.Domain.Dtos.Holidays;
using BusinessHours.Domain.Errors;

namespace BusinessHours.Domain.Dtos.Validators
{
    public static class HolidayDtosValidators
    {
        static readonly Regex HOLIDAY_TIME_PATTERN = new Regex(@"^(?<hours>([01]\d)|(2[0-3])):(?<minutes>[0-5]\d)$");

        public static void Validate(this HolidayCreateDto obj)
        {
            var now = DateTime.Now;
            if (string.IsNullOrEmpty(obj.Name)) throw new MissingBodyParamException("name");
            if (obj.Year.HasValue && obj.Year.Value < 0) throw new InvalidBodyParamException("year");
            if (obj.Year.HasValue && obj.Year.Value < now.Year) throw new InvalidBodyParamException("year");
            if (obj.Month < 1 || obj.Month > 12) throw new InvalidBodyParamException("month");
            if (obj.Day < 1 || obj.Day > 31) throw new InvalidBodyParamException("day");
            var year = obj.Year.HasValue ? obj.Year.Value : now.Year;
            var validateDate = new DateTime(year, obj.Month, obj.Day);
            if (!obj.AllDay)
            {
                if (string.IsNullOrEmpty(obj.Start)) throw new MissingBodyParamException($"start");
                if (!HOLIDAY_TIME_PATTERN.IsMatch(obj.Start)) throw new InvalidBodyParamException($"start");
                if (string.IsNullOrEmpty(obj.Finish)) throw new MissingBodyParamException($"finish");
                if (!HOLIDAY_TIME_PATTERN.IsMatch(obj.Finish)) throw new InvalidBodyParamException($"finish");
            }
        }

        public static void Validate(this HolidayUpdateDto obj)
        {
            var now = DateTime.Now;
            if (obj.Year.HasValue && obj.Year.Value < 0) throw new InvalidBodyParamException("year");
            if (obj.Year.HasValue && obj.Year.Value < now.Year) throw new InvalidBodyParamException("year");
            if (obj.Month.HasValue && obj.Month.Value < 1 || obj.Month.Value > 12) throw new InvalidBodyParamException("month");
            if (obj.Day.HasValue && obj.Day.Value < 1 || obj.Day.Value > 31) throw new InvalidBodyParamException("day");
            var year = obj.Year.HasValue ? obj.Year.Value : now.Year;
            var month = obj.Month.HasValue ? obj.Month.Value : now.Month;
            var day = obj.Day.HasValue ? obj.Day.Value : now.Day;
            var validateDate = new DateTime(year, month, day);
            if (obj.AllDay.HasValue && !obj.AllDay.Value)
            {
                if (string.IsNullOrEmpty(obj.Start)) throw new MissingBodyParamException($"start");
                if (!HOLIDAY_TIME_PATTERN.IsMatch(obj.Start)) throw new InvalidBodyParamException($"start");
                if (string.IsNullOrEmpty(obj.Finish)) throw new MissingBodyParamException($"finish");
                if (!HOLIDAY_TIME_PATTERN.IsMatch(obj.Finish)) throw new InvalidBodyParamException($"finish");
            }
        }
    }
}
