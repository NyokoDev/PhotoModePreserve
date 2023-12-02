namespace DynamicResolutionExtended
{

    using Colossal.Logging;
    using Game;
    using Game.Modding;
    using DynamicResolutionExtended.Systems;

    public sealed class Mod : IMod
    {

        // - Start of mod properties.
        public const string ModName = "Photo Mode Preserve";                    
        public static Mod Instance { get; private set; }
        internal ILog Log { get; private set; }
        public void OnLoad()
        {
            Instance = this;
            Log = LogManager.GetLogger(ModName);
            Log.Info("setting logging level to Debug");
            Log.effectivenessLevel = Level.Debug;

            Log.Info("loading");
            Log.Info("=======PHOTO MODE PRESERVE=======");
            Log.Info("=======     Initialized   =======");
            Log.Info("=======                   =======");
        }

        //End of mod properties.

        /// <summary>
        /// Called by the game when the game world is created. 
        /// </summary>
        /// <param name="updateSystem">Game update system.</param>
        public void OnCreateWorld(UpdateSystem updateSystem)
        {
            Log.Info("[DYNRESEXT] starting OnCreateWorld");

   
 

        }

        /// <summary>
        /// Called by the game when the mod is disposed of.
        /// </summary>
        public void OnDispose()
        {
            Log.Info("disposing");
            Instance = null;
        }
    }
}