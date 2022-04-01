using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BusinessHours.Domain.Errors;

namespace BusinessHours.Domain.Dtos.Validators
{
    public static class BusinessHoursRuleDtoValidators
    {
        static readonly Regex BUSINESS_HOURS_RULE_TIME_PATTERN = new Regex(@"^(?<hours>([01]\d)|(2[0-3])):(?<minutes>[0-5]\d)$");

        public static void Validate(this BusinessHoursRuleCreateDto obj)
        {
            if (string.IsNullOrEmpty(obj.Name)) throw new MissingBodyParamException("name");
            if (string.IsNullOrEmpty(obj.Timezone)) throw new MissingBodyParamException("timezone");

            var timezone = TimeZoneInfo.FindSystemTimeZoneById(obj.Timezone);

            if (obj.WorkHours == null || obj.WorkHours.Count == 0) throw new MissingBodyParamException("workHours");

            var alreadyUsedDays = new List<DayOfWeek>();
            for (int i = 0; i < obj.WorkHours.Count(); i++)
            {
                var item = obj.WorkHours[i];
                if (item == null) throw new InvalidBodyParamException($"workHours.{i}");
                if (string.IsNullOrEmpty(item.Day)) throw new MissingBodyParamException($"workHours.{i}.day");
                if (!Enum.IsDefined(typeof(DayOfWeek), item.Day)) throw new InvalidBodyParamException($"workHours.{i}.day");
                var day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.Day, true);
                if (alreadyUsedDays.Contains(day)) throw new BadRequestException($"Invalid body param: workHours.{i}.day. Duplicated entries.");
                alreadyUsedDays.Add(day);
                if (item.Open)
                {
                    if (string.IsNullOrEmpty(item.Start)) throw new MissingBodyParamException($"workHours.{i}.start");
                    if (!BUSINESS_HOURS_RULE_TIME_PATTERN.IsMatch(item.Start)) throw new InvalidBodyParamException($"workHours.{i}.start");
                    if (string.IsNullOrEmpty(item.Finish)) throw new MissingBodyParamException($"workHours.{i}.finish");
                    if (!BUSINESS_HOURS_RULE_TIME_PATTERN.IsMatch(item.Finish)) throw new InvalidBodyParamException($"workHours.{i}.finish");
                }
            }
        }

        public static void Validate(this BusinessHoursRuleUpdateDto obj)
        {
            if (obj.Name != null && string.IsNullOrEmpty(obj.Name)) throw new InvalidBodyParamException("name");
            if (obj.Timezone != null && string.IsNullOrEmpty(obj.Timezone)) throw new InvalidBodyParamException("timezone");

            var timezone = TimeZoneInfo.FindSystemTimeZoneById(obj.Timezone);

            if (obj.WorkHours != null)
            {
                var alreadyUsedDays = new List<DayOfWeek>();
                for (int i = 0; i < obj.WorkHours.Count(); i++)
                {
                    var item = obj.WorkHours[i];
                    if (item == null) throw new InvalidBodyParamException($"workHours.{i}");
                    if (string.IsNullOrEmpty(item.Day.ToString())) throw new MissingBodyParamException($"workHours.{i}.day");
                    if (!Enum.IsDefined(typeof(DayOfWeek), item.Day)) throw new InvalidBodyParamException($"workHours.{i}.day");
                    var day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.Day, true);
                    if (alreadyUsedDays.Contains(day)) throw new BadRequestException($"Invalid body param: workHours.{i}.day. Duplicated entries.");
                    alreadyUsedDays.Add(day);
                    if (item.Open)
                    {
                        if (string.IsNullOrEmpty(item.Start)) throw new MissingBodyParamException($"workHours.{i}.start");
                        if (!BUSINESS_HOURS_RULE_TIME_PATTERN.IsMatch(item.Start)) throw new InvalidBodyParamException($"workHours.{i}.start");
                        if (string.IsNullOrEmpty(item.Finish)) throw new MissingBodyParamException($"workHours.{i}.finish");
                        if (!BUSINESS_HOURS_RULE_TIME_PATTERN.IsMatch(item.Finish)) throw new InvalidBodyParamException($"workHours.{i}.finish");
                    }
                }
            }
        }
    }
}
