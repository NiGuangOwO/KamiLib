﻿using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Game.Text.SeStringHandling.Payloads;
using KamiLib.Localization;

namespace KamiLib.Game;

public static class Chat
{
    public static void Print(string tag, string message)
    {
        Service.Chat.Print(GetBaseString(tag, message).BuiltString, KamiCommon.PluginName, 45);
    }

    public static void Print(string tag, string message, DalamudLinkPayload? payload)
    {
        if (payload is null)
        {
            Print(tag, message);
            return;
        }

        Service.Chat.Print(GetBaseString(tag, message, payload).BuiltString, KamiCommon.PluginName, 45);
    }

    public static void PrintError(string message)
    {
        Service.Chat.PrintError(GetBaseString(Strings.Common_Error, message).BuiltString);
    }

    private static SeStringBuilder GetBaseString(string tag, string message, DalamudLinkPayload? payload = null)
    {
        if (payload is null)
        {
            return new SeStringBuilder()
                .AddUiForeground($"[{tag}] ", 62)
                .AddText(message);
        }
        return new SeStringBuilder()
            .AddUiForeground($"[{tag}] ", 62)
            .Add(payload)
            .AddUiForeground(message, 35)
            .Add(RawPayload.LinkTerminator);
    }
}