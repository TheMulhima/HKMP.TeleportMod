using System;
using System.Linq;
using HKMP_Teleport.Events;

namespace HKMP_Teleport.Commands;

public class TeleportCommand : Hkmp.Api.Command.Client.IClientCommand
{
    public string Trigger { get; } = "teleport";
    public string[] Aliases { get; } = new[] 
    { 
        "Teleport",
        "tp", "Tp", 
        "/tp", @"\tp", "/Tp", @"\Tp",
        "/teleport", @"\teleport", "/Teleport", @"\Teleport",
    };

    public void Execute(string[] arguments)
    {
        if (arguments.Length == 1)
        {
            HKMP_TeleportMod.Pipe.ClientApi.UiManager.ChatBox.AddMessage("[Teleport Mod] Error: please provide a player name");
            return;
        }

        var name = arguments.Skip(1).Aggregate("", (current, arg) => current + arg + " ");
        name = name.Remove(name.Length - 1, 1); //remove the last extra space at the end

        var player = HKMP_TeleportMod.Pipe.ClientApi.ClientManager.Players.FirstOrDefault(x => x.Username.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (player == null)
        {
            HKMP_TeleportMod.Pipe.ClientApi.UiManager.ChatBox.AddMessage($"[Teleport Mod] Error: {name} is not a player in this server");
            return;
        }
        
        HKMP_TeleportMod.Pipe.SendToPlayer(player.Id, new TeleportRequestEvent(), SameScene:false);
    }
}