using Game;
using Game.Modding;
using Colossal.Logging;

namespace PedestrianPathfinding;

public class Mod : IMod
{
    public static ILog log = LogManager.GetLogger(nameof(PedestrianPathfinding)).SetShowsErrorsInUI(true);

    public void OnLoad(UpdateSystem updateSystem)
    {
        log.Info("Loading PedestrianPathfinding");

        updateSystem.UpdateAfter<PedestrianPathfindSystem>(SystemUpdatePhase.LoadSimulation);
    }

    public void OnDispose()
    {
    }
}