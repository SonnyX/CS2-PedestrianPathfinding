using Game;
using Game.Modding;
using Colossal.Logging;
using Game.SceneFlow;
using HarmonyLib;

namespace PedestrianPathfinding
{
    public class Mod : IMod
    {
        public static ILog log = LogManager.GetLogger(nameof(PedestrianPathfinding)).SetShowsErrorsInUI(true);

        public void OnLoad(UpdateSystem updateSystem)
        {
            log.Info("Loading PedestrianPathfinding");

            var harmony = new Harmony("com.github.sonnyx.pedestrianpathfinding");
            harmony.PatchAll(typeof(Mod).Assembly);

            log.Info($"Harmony patches loaded");

            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                log.Info($"Current mod asset at {asset.path}");
        }

        public void OnDispose()
        {
        }
    }
}