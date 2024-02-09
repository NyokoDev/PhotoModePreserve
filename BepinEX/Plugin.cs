using BepInEx;
using Game.Vehicles;
using Game;
using UnityEngine;
#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif

namespace PhotoModePreserve
{
    using System.Collections.Generic;
    using System.Reflection;
    using BepInEx;
    using PhotoModePreserve.Systems;
    using Game;
    using Game.Common;
    using Game.PSI;
    using Game.Rendering;
    using Game.Rendering.CinematicCamera;
    using Game.SceneFlow;
    using Game.UI.InGame;
    using HarmonyLib;
    using System;

    [BepInPlugin(GUID, "Photo Mode Preserve", "1.1")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.nyoko.photomodepreserve";
        private Mod _mod;
        public bool Compatible;
        private PhotoModeUISystem m_PhotoModeUISystem;

        private static CameraUpdateSystem m_CameraUpdateSystem;
        public static bool orbitMode { get; set; }

        public void Awake()
        {

            _mod = new();
            _mod.OnLoad();
            // Apply Harmony patches.
            string gameVersion = Application.version.ToString();
            if (gameVersion.Equals("1.0.19f1"))
            {
                Compatible = true;
            }
            if (Compatible) { 
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
                Console.WriteLine(" ────────────────────────────────── ");
                Console.WriteLine(" ────────────────────────────────── ");
                Console.WriteLine("Preserve Photo Mode 1.1", ConsoleColor.Blue, ConsoleColor.Blue);
                Console.WriteLine("Support: https://discord.gg/5gZgRNm29e", ConsoleColor.Blue, ConsoleColor.Blue);
                Console.WriteLine("Donate: https://shorturl.at/hmpCW", ConsoleColor.Blue, ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" ────────────────────────────────── ");
                Console.WriteLine(" PPM doesn't save lightning values. Consider looking at ColorAdjustments mod instead.");
                Console.WriteLine("https://thunderstore.io/c/cities-skylines-ii/p/Nyoko/ColorAdjustments/");
                Console.WriteLine(" ────────────────────────────────── ");
                Console.ResetColor();
            }
            else
            {
                UnityEngine.Debug.Log("Preserve Photo Mode is not compatible with your current game version. Contact the developer https://discord.gg/5gZgRNm29e ");
            }
        }

        /// <summary>
        /// Harmony postfix to <see cref="SystemOrder.Initialize"/> to substitute for IMod.OnCreateWorld.
        /// </summary>
        /// <param name="updateSystem"><see cref="GameManager"/> <see cref="UpdateSystem"/> instance.</param>
        [HarmonyPatch(typeof(SystemOrder), nameof(SystemOrder.Initialize))]
        [HarmonyPostfix]
        private static void InjectSystems(UpdateSystem updateSystem) => Mod.Instance.OnCreateWorld(updateSystem);

        [HarmonyPatch(typeof(PhotoModeUISystem), nameof(PhotoModeUISystem.Activate))]
        [HarmonyPrefix]
        public static bool OverrideActivate(PhotoModeRenderSystem ___m_PhotoModeRenderSystem)
        {
            ___m_PhotoModeRenderSystem.Enable(true);
            return false;
        }

        [HarmonyPatch(typeof(PhotoModeRenderSystem), nameof(PhotoModeRenderSystem.DisableAllCameraProperties))]
        [HarmonyPrefix]
        public static bool DontDisableAllCameraPropertiesPlease(Game.Rendering.PhotoModeRenderSystem __instance)
        {
            foreach (KeyValuePair<string, PhotoModeProperty> photoModeProperty in __instance.photoModeProperties)
            {
                photoModeProperty.Value.setEnabled?.Invoke(obj: true);
            }

            return false;
        }
    }
}

// Code authored by Nyoko
// Feel free to reach out for any questions or clarifications!
// Huge thanks to Algernon for input and assistance on mod structure plus photo mode render system prefix implementation.
