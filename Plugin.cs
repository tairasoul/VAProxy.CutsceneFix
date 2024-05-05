using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace CutsceneFix
{
    [BepInPlugin("tairasoul.vaproxy.cutscenefix", "CutsceneFix", "1.0.1")]
    internal class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource log;
        void Start()
        {
            log = Logger;
            Harmony harm = new("CutsceneFix");
            harm.PatchAll();
        }
    }
}