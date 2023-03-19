using System.Collections;
using GlobalEnums;
using HKMirror.Reflection.SingletonClasses;
using HKMirror.Reflection;
using UnityEngine;

namespace HKMP_Teleport
{
    public static class Teleporter
    {
        public static string PreviousScene;
        public static Vector3 PreviousPos;

        internal static IEnumerator TeleportCoro(string scene, Vector3 pos)
        {
            HeroControllerR.IgnoreInputWithoutReset();

            HeroControllerR.CancelSuperDash();
            HeroControllerR.ResetMotion();
            HeroControllerR.airDashed = false;
            HeroControllerR.doubleJumped = false;
            HeroControllerR.AffectedByGravity(false);


            //yes this is a savestate load
            GameManager.instance.entryGateName = "dreamGate";
            GameManager.instance.startedOnThisScene = true;

            GameManager.instance.BeginSceneTransition
            (
                new GameManager.SceneLoadInfo
                {
                    SceneName = scene,
                    HeroLeaveDirection = GatePosition.unknown,
                    EntryGateName = "dreamGate",
                    EntryDelay = 0f,
                    WaitForSceneTransitionCameraFade = false,
                    Visualization = 0,
                    AlwaysUnloadUnusedAssets = true
                }
            );

            GameManager.instance.cameraCtrl.Reflect().isGameplayScene = true;

            GameManager.instance.cameraCtrl.PositionToHero(false);

            yield return new WaitUntil(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == scene);

            GameManager.instance.cameraCtrl.FadeSceneIn();

            HeroControllerR.TakeMP(1);
            HeroControllerR.AddMPChargeSpa(1);
            HeroControllerR.TakeHealth(1);
            HeroControllerR.AddHealth(1);

            GameCameras.instance.hudCanvas.gameObject.SetActive(true);

            GameManager.instance.cameraCtrl.Reflect().isGameplayScene = true;

            yield return null;

            HeroControllerR.transform.position = pos;

            HeroControllerR.cState.inConveyorZone = false;
            HeroControllerR.cState.onConveyor = false;
            HeroControllerR.cState.onConveyorV = false;
            HeroControllerR.FinishedEnteringScene(true, false);
            yield return null;

            GameCameras.instance.StopCameraShake();
        }
    }
}