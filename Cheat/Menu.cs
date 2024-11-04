using GorillaNetworking;
using J0kersGuardianMenu.Cheat;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace J0kersGuardianMenu
{
    public class Menu : MonoBehaviour
    {
        private bool GGSpam;

        public void OnGUI()
        {
            GUI.backgroundColor = Color.Lerp(Color.magenta, Color.blue, Mathf.PingPong(Time.time, 1f));

            GUILayout.BeginArea(new Rect(10, 10, 200, 300), GUI.skin.box);

            GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 20;
            titleStyle.alignment = TextAnchor.UpperCenter;

            GUILayout.Label("J0kers Guardian Panel", titleStyle);

            if (GUILayout.Button("Become Guardian", GUILayout.Height(40)))
            {
                GGSpam = !GGSpam;
            }
            if (GGSpam)
            {
                Mods.AlwaysGuardian();
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }

            if (GUILayout.RepeatButton("Fling All [Hold]", GUILayout.Height(40)))
            {
                Mods.FlingPlayers();
            }

            if (GUILayout.Button("Press GameMode Button", GUILayout.Height(40)))
            {
                foreach (ModeSelectButton mode in Object.FindObjectsOfType(typeof(ModeSelectButton)))
                {
                    if (mode.gameMode.Contains("GUARDIAN"))
                    {
                        mode.ButtonActivationWithHand(false);
                    }
                }
            }

            if (GUILayout.Button("Leave", GUILayout.Height(40)))
            {
                PhotonNetwork.Disconnect();
            }
            GUILayout.EndArea();

            //======= < PLAYER SIZE BOX > =======

            // Sorry This Code Is Very Ugly!
            GUILayout.BeginArea(new Rect(220, 10, 250, 300), GUI.skin.box);

            GUILayout.Label("Player Sizer [M]", titleStyle);

            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label("Player " + i + ": " + PhotonNetwork.PlayerList[i].NickName);
                Photon.Realtime.Player player = PhotonNetwork.PlayerList[i];

                if (GUILayout.Button("Size Player"))
                {
                    if (PhotonNetwork.IsMasterClient) // Have to be master =(
                    {
                        foreach (GorillaGuardianZoneManager gorillaGuardianZoneManager in GorillaGuardianZoneManager.zoneManagers)
                        {
                            if (gorillaGuardianZoneManager.enabled)
                            {
                                gorillaGuardianZoneManager.SetGuardian(player);
                            }
                        }
                    }
                }

                // What this does is it makes a new Gui Text Lable & Button for every player in the lobby from there if you are the master of the room you can pick anyones size
                GUILayout.EndHorizontal();
            }

            GUILayout.EndArea();
        }
    }
}