﻿using System;
using System.Globalization;
using System.Reflection;
using ImGuiNET;

namespace KamiLib.AutomaticUserInterface;

/// <summary>
///     Displays the tagged date time as it was saved
/// </summary>
public class DateTimeDisplayAttribute : LeftLabeledTabledDrawableAttribute
{
    public DateTimeDisplayAttribute(string? label) : base(label)
    {
    }

    protected override void DrawRightColumn(object obj, MemberInfo field, Action? saveAction = null)
    {
        var dateTime = GetValue<DateTime>(obj, field);

        ImGui.TextUnformatted(FormatDateTime(dateTime));
    }

    protected virtual string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToString(CultureInfo.CurrentCulture);
    }
}