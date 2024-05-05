using HarmonyLib;
using CutsceneFix.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CutsceneFix.Patches
{
    [HarmonyPatch(typeof(Dim))]
    static class DimPatch
    {
        [HarmonyPatch("Action")]
        [HarmonyPrefix]
        static bool ActionPrefix(Dim __instance, int shot)
        {
            ReflectionHelper helper = new(__instance);
            Time.timeScale = 1f;
            __instance.Skip.SetActive(value: true);
            __instance.GetComponent<Meditation>().EndRipple();
            __instance.skip = false;
            __instance.movie = true;
            __instance.cam.transform.parent = null;
            __instance.cam.near = false;
            __instance.cam.enabled = false;
            helper.SetField("NearClip", Camera.main.nearClipPlane);
            Camera.main.nearClipPlane = 0.01f;
            __instance.UI.SetActive(value: false);
            Inventory player = helper.GetField<Inventory>("player");
            player.drone.shooter.StopShooting();
            player.inp.SetLockAllInput(value: true);
            player.GetComponent<Rigidbody>().isKinematic = true;
            Animator component = player.GetComponent<Animator>();
            component.updateMode = AnimatorUpdateMode.AnimatePhysics;
            component.Play("Null", 1);
            component.Play("Null", 2);
            component.Play("Null", 3);
            component.Play("Null", 4);
            component.Play("Null", 5);
            component.Play("Null", 6);
            component.Play("Null", 7);
            player.GetComponent<AntiClip>().enabled = false;
            __instance.cam.GetComponent<Animator>().enabled = true;
            helper.SetField("id", shot);
            __instance.StopAllCoroutines();
            __instance.StartCoroutine(MovieReplacement(__instance, shot));
            return false;
        }

        static IEnumerator MovieReplacement(Dim instance, int shot)
        {
            Plugin.log.LogInfo("MovieReplacement called!");
            for (int i = 0; i < instance.Shots[shot].Stuff.Length; i++)
            {
                if (!instance.skip)
                    yield return new WaitForSeconds(instance.Shots[shot].delay[i]);
                else
                    yield return new WaitForEndOfFrame();
                Plugin.log.LogInfo($"Invoking Shots[{shot}].Stuff[{i}].");
                instance.Shots[shot].Stuff[i].Invoke();
                Plugin.log.LogInfo($"Invoked Shots[{shot}].Stuff[{i}].");
            }
            ReflectionHelper helper = new(instance);
            if (helper.GetField<int>("id") != 3)
            {
                instance.ActionEnd();
                yield break;
            }
            instance.movie = false;
            instance.Skip.SetActive(false);
        }
    }
}