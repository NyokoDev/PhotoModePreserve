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

    [BepInPlugin(GUID, "Photo Mode Preserve", "1.0")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.nyoko.photomodepreserve";
        private Mod _mod;
        private PhotoModeUISystem m_PhotoModeUISystem;

        private static CameraUpdateSystem m_CameraUpdateSystem;
        public static bool orbitMode { get; set; }

        public void Awake()
        {

            _mod = new();
            _mod.OnLoad();

            _mod.Log.Info("=======PHOTO MODE PRESERVE=======");
            _mod.Log.Info("=======     Initialized   =======");
            _mod.Log.Info("=======                   =======");

            // Apply Harmony patches.
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
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
