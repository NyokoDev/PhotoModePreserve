using BepInEx;
#if BEPINEX_V6
    using BepInEx.Unity.Mono;
#endif
namespace DynamicResolutionExtended
{
    using System.Reflection;
    using BepInEx;
    using DynamicResolutionExtended.Systems;
    using Game;
    using Game.Common;
    using Game.Rendering;
    using Game.SceneFlow;
    using Game.UI.InGame;
    using HarmonyLib;

    [BepInPlugin(GUID, "Photo Mode Preserve", "1.0")]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "com.nyoko.photomodepreserve";
        private Mod _mod;
   

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

        [HarmonyPatch(typeof(PhotoModeRenderSystem), nameof(PhotoModeRenderSystem.DisableAllCameraProperties))]
        [HarmonyPostfix]
        public static void ActivatePatch()
        {
            DyUISystem.Instance.DisableAllCameraProperties();
        }
   
    }
}
// Code authored by Nyoko
// Feel free to reach out for any questions or clarifications!
// Huge thanks to Algernon for input on mod structure.
