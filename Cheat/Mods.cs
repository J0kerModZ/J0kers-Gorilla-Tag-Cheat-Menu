using HarmonyLib;
using Photon.Pun;
using UnityEngine;

namespace J0kersGuardianMenu.Cheat
{
    public class Mods : MonoBehaviour
    {
        public static float TapDelay = 0f;
        public static void AlwaysGuardian()
        {
            if (PhotonNetwork.IsMasterClient) // If The Local Player Is Master It Just Sets The Guardian To Your Self
            {
                foreach (GorillaGuardianZoneManager gorillaGuardianZoneManager in GorillaGuardianZoneManager.zoneManagers)
                {
                    if (gorillaGuardianZoneManager.enabled)
                    {
                        gorillaGuardianZoneManager.SetGuardian(NetworkSystem.Instance.LocalPlayer);
                    }
                }
            }
            else // If Not Master Then It Will Tap The Moon Thing
            {
                foreach (TappableGuardianIdol tappableGuardianIdol in Object.FindObjectsOfType(typeof(TappableGuardianIdol)))
                {
                    if (tappableGuardianIdol.enabled && tappableGuardianIdol.isActivationReady)
                    {
                        GorillaGuardianManager gorillaGuardianManager = FindObjectOfType<GorillaGuardianManager>();
                        if (!gorillaGuardianManager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = false;
                            GorillaTagger.Instance.offlineVRRig.transform.position = tappableGuardianIdol.transform.position;

                            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.forward * 3f;
                            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.forward * 3f;

                            GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
                            GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.rotation = Quaternion.Euler(new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f)));
                            if (Time.time > TapDelay)
                            {
                                TapDelay = Time.time + 0.27f;
                                tappableGuardianIdol.OnTap(UnityEngine.Random.Range(0.2f, 0.4f));
                            }
                        }
                        else
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
        }

        public static void FlingPlayers()
        {
            GorillaGuardianManager gorillaGuardianManager = FindObjectOfType<GorillaGuardianManager>();
            if (gorillaGuardianManager.IsPlayerGuardian(NetworkSystem.Instance.LocalPlayer))
            {
                foreach (VRRig VrRigs in GorillaParent.instance.vrrigs)
                {
                    // Skip the local player to prevent flinging yourself
                    if (VrRigs != GorillaTagger.Instance.offlineVRRig && VrRigs.OwningNetPlayer != NetworkSystem.Instance.LocalPlayer)
                    {
                        // Send RPCs only to other players
                        NetViewInVRRig(VrRigs).SendRPC("GrabbedByPlayer", RpcTarget.All, new object[] { true, false, false });

                        NetViewInVRRig(VrRigs).SendRPC("DroppedByPlayer", RpcTarget.All, new object[] { new Vector3(0f, 20f, 0f) }); // Flings them on the Y-axis (20f is max)
                    }
                }
            }
        }

        public static NetworkView NetViewInVRRig(VRRig vRRig)
        {
            return (NetworkView)Traverse.Create(vRRig).Field("netView").GetValue(); // retrieves NetworkView component from VRRig 
        }
    }
}
