﻿using System;
using System.Linq;
using KamiLib.Caching;
using Lumina.Excel.GeneratedSheets;

namespace KamiLib.Utilities;

public static class Time
{
    private static DateTime GetNextDateTimeForHour(int hours) => DateTime.UtcNow.Hour < hours ? DateTime.UtcNow.Date.AddHours(hours) : DateTime.UtcNow.Date.AddDays(1).AddHours(hours);

    public static DateTime NextDailyReset() => GetNextDateTimeForHour(15);
    public static DateTime NextWeeklyReset() => NextDayOfWeek(DayOfWeek.Tuesday, 8);
    public static DateTime NextFashionReportReset() => NextWeeklyReset().AddDays(3);
    public static DateTime NextGrandCompanyReset() => GetNextDateTimeForHour(20);

    public static DateTime NextLeveAllowanceReset()
    {
        var now = DateTime.UtcNow;

        return now.Hour < 12 ? now.Date.AddHours(12) : now.Date.AddDays(1);
    }

    public static DateTime NextDayOfWeek(DayOfWeek weekday, int hour)
    {
        var today = DateTime.UtcNow;
            
        if(today.Hour < hour && today.DayOfWeek == weekday)
        {
            return today.Date.AddHours(hour);
        }
        else
        {
            var nextReset = today.AddDays(1);

            while (nextReset.DayOfWeek != weekday)
            {
                nextReset = nextReset.AddDays(1);
            }
                
            return nextReset.Date.AddHours(hour);
        }
    }

    public static DateTime NextJumboCactpotReset()
    {
        var region = LookupDatacenterRegion(Service.ClientState.LocalPlayer?.HomeWorld.GameData?.DataCenter.Row);

        return region switch
        {
            // Japan
            1 => NextDayOfWeek(DayOfWeek.Saturday, 12),

            // North America
            2 => NextDayOfWeek(DayOfWeek.Sunday, 2),

            // Europe
            3 => NextDayOfWeek(DayOfWeek.Saturday, 19),

            // Australia
            4 => NextDayOfWeek(DayOfWeek.Saturday, 9),

            // China
            _ => NextDayOfWeek(DayOfWeek.Saturday, 13),
        };
    }

    private static byte LookupDatacenterRegion(uint? playerDatacenterID)
    {
        if (playerDatacenterID == null) return 0;

        return LuminaCache<WorldDCGroupType>.Instance
            .Where(world => world.RowId == playerDatacenterID.Value)
            .Select(dc => dc.Region)
            .FirstOrDefault();
    }
}