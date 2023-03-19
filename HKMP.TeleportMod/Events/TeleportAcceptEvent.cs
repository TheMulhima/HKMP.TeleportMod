using System;
using HKMP.TeleportMod;
using HkmpPouch;
using UnityEngine;

namespace HKMP_Teleport.Events;

public class TeleportAcceptEvent : PipeEvent
{
    public static string Seperator = "&*&*&()(())";
    public static string Name = "TeleportRequest";
    public override string GetName() => Name;

    public Vector3 TargetPosition;
    public string TargetSceneName;

    public TeleportAcceptEvent(string targetSceneName, Vector3 targetPosition)
    {
        TargetSceneName = targetSceneName;
        TargetPosition = targetPosition;
    }

    public override string ToString()
    {
        return $"{TargetSceneName}{Seperator}{TargetPosition}";
    }
}
public class TeleportAcceptEventFactory : IEventFactory
{
    public static TeleportAcceptEventFactory Instance = new TeleportAcceptEventFactory();

    public PipeEvent FromSerializedString(string serializedData)
    {
        var data = serializedData.Split(new[] { TeleportAcceptEvent.Seperator }, StringSplitOptions.None);
        return new TeleportAcceptEvent(data[0], data[1].StringToVector3());
    }

    public string GetName() => TeleportAcceptEvent.Name;
}