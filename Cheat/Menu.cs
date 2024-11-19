using GorillaNetworking;
using J0kersGuardianMenu.Cheat;
using Photon.Pun;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace J0kersGuardianMenu
{
    public class Menu : MonoBehaviour
    {
        static bool Spam;
        static bool spamOthers;

        public void OnGUI()
        {
            GUI.backgroundColor = Color.Lerp(Color.magenta, Color.blue, Mathf.PingPong(Time.time, 1f));

            GUILayout.BeginArea(new Rect(10, 10, 200, 300), GUI.skin.box);

            GUIStyle titleStyle = new GUIStyle(GUI.skin.label);
            titleStyle.fontSize = 20;
            titleStyle.alignment = TextAnchor.UpperCenter;

            GUILayout.Label("J0kers Lego Panel", titleStyle);

            if (GUILayout.Button("Block Spammer [BIG]", GUILayout.Height(40)))
            {
                Spam = !Spam;
            }
            if (Spam)
            {
                Mods.BlockSpammer();
            }

            if (GUILayout.Button("Block Spammer Others [BIG]", GUILayout.Height(40)))
            {
                spamOthers = !spamOthers;
            }
            if (spamOthers)
            {
                Mods.BlockSpammerOther();
            }

            if (GUILayout.Button("Leave", GUILayout.Height(40)))
            {
                PhotonNetwork.Disconnect();
            }
            GUILayout.EndArea();
        }
    }
}