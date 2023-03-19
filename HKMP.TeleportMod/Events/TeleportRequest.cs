using System.Globalization;
using HkmpPouch;

namespace HKMP_Teleport.Events;

public class TeleportRequestEvent : PipeEvent
{
    public static string Name = "TeleportRequest";
    public override string GetName() => Name;

    public override string ToString()
    {
        return string.Empty;
    }
}
public class TeleportRequestEventFactory : IEventFactory
{
    public static TeleportRequestEventFactory Instance = new TeleportRequestEventFactory();

    public PipeEvent FromSerializedString(string serializedData)
    {
        return new TeleportRequestEvent();
    }

    public string GetName() => TeleportRequestEvent.Name;
}