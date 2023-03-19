using HKMP_Teleport.Commands;
using HKMP_Teleport.Events;
using HkmpPouch;
using Modding;
using Satchel;

namespace HKMP_Teleport;

public class HKMP_TeleportMod : Mod
{
    internal static HKMP_TeleportMod Instance;
    public override string GetVersion() => AssemblyUtils.GetAssemblyVersionHash();
    public new string GetName() => "HKMP.Teleport";
    
    internal static PipeClient Pipe;

    public override void Initialize()
    {
        Instance ??= this;

        ModHooks.FinishedLoadingModsHook += () =>
        {
            Pipe = new PipeClient(GetName());
            Pipe.ClientApi.CommandManager.RegisterCommand(new TeleportCommand());
            Pipe.On(TeleportRequestEventFactory.Instance).Do<TeleportRequestEvent>(e =>
            {
                Pipe.SendToPlayer(e.FromPlayer,
                    new TeleportAcceptEvent(GameManager.instance.sceneName,
                        HeroController.instance.transform.position), SameScene:false);
            });
            Pipe.On(TeleportAcceptEventFactory.Instance).Do<TeleportAcceptEvent>(e =>
            {
                GameManager.instance.StartCoroutine(Teleporter.TeleportCoro(e.TargetSceneName, e.TargetPosition));
            });
        };
    }
}