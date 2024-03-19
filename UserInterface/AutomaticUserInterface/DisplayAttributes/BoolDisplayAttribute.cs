﻿using System;
using System.Drawing;
using System.Reflection;
using Dalamud.Interface;
using ImGuiNET;

namespace KamiLib.AutomaticUserInterface;

public class BoolDisplayAttribute : LeftLabeledTabledDrawableAttribute
{
    protected KnownColor FalseColor = KnownColor.White;
    protected string FalseString = "False";

    protected KnownColor TrueColor = KnownColor.White;
    protected string TrueString = "True";

    public BoolDisplayAttribute(string? label) : base(label)
    {
    }

    protected override void DrawRightColumn(object obj, MemberInfo field, Action? saveAction = null)
    {
        var boolData = GetValue<bool>(obj, field);

        var color = boolData ? TrueColor.Vector() : FalseColor.Vector();
        var label = boolData ? TrueString : FalseString;

        ImGui.TextColored(color, label);
    }
}