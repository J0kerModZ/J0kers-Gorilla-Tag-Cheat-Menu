using BepInEx;
using HarmonyLib;
using J0kersGuardianMenu.Cheat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace J0kersGuardianMenu.Loading
{
    [BepInPlugin("com.J0kerMenu.J0kerModZ", "J0kerMenu", "1.0.0")]
    [HarmonyPatch(typeof(GorillaLocomotion.Player), "LateUpdate", MethodType.Normal)]
    public class Loader : BaseUnityPlugin
    {
        public void FixedUpdate()
        {
            if (!GameObject.Find("J0kerLoader") && GorillaLocomotion.Player.hasInstance)
            {
                GameObject Loader = new GameObject("J0kerLoader");
                Loader.AddComponent<Menu>();
                Loader.AddComponent<Mods>();
            }
        }
    }
}
