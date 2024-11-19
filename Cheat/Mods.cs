using GorillaTagScripts;
using Photon.Pun;
using System.Collections;
using UnityEngine;

namespace J0kersGuardianMenu.Cheat
{
    public class Mods : MonoBehaviour
    {
        private static bool LostFrame;
        static float SpamTime = 0f;
        private static float rpcBypassCooldown = 5f;
        private static float lastRpcBypassTime;


        public static void RPCBypass()
        {
            if (Time.time - lastRpcBypassTime < rpcBypassCooldown)
                return;

            lastRpcBypassTime = Time.time;

            try
            {
                if (!LostFrame)
                {
                    LostFrame = true;
                    PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);

                    GorillaNot.instance.logErrorMax = 1000;
                    GorillaNot.instance.rpcErrorMax = 1000;
                    GorillaNot.instance.rpcCallLimit = 1000;

                    PhotonNetwork.MaxResendsBeforeDisconnect = 5;
                    PhotonNetwork.QuickResends = 2;

                    GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"RPCBypass Error: {ex.Message}");
            }
        }

        public static void BlockSpammer()
        {
            if (Time.time > SpamTime)
            {
                BuilderTable.instance.RequestCreatePiece(-566818631, GorillaTagger.Instance.offlineVRRig.transform.position, GorillaTagger.Instance.offlineVRRig.transform.rotation, -1);
                RPCBypass();
                SpamTime = Time.time + 0.1f;
            }
        }

        public static void BlockSpammerOther()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    if (Time.time > SpamTime)
                    {
                        BuilderTable.instance.RequestCreatePiece(-566818631, rig.transform.position, rig.transform.rotation, -1);
                        RPCBypass();
                        SpamTime = Time.time + 0.1f;
                    }
                }
            }
        }
    }
}
