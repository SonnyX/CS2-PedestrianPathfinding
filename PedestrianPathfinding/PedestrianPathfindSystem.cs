using Colossal.Logging;
using Game;
using Game.Pathfind;
using Unity.Entities;

namespace PedestrianPathfinding;

public partial class PedestrianPathfindSystem : GameSystemBase
{
    // We can remove the GetUpdateInterval and OnUpdate methods
    // if they are empty, as the base class handles them.

    protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
    {
        base.OnGameLoadingComplete(purpose, mode);
        ILog log = LogManager.GetLogger(nameof(PedestrianPathfinding)).SetShowsErrorsInUI(true);

        // We only want this logic to run when loading a game, not in the editor
        if (mode != GameMode.Game)
        {
            return;
        }

        log.Info("Applying custom pedestrian pathfinding costs...");

        // This is a more direct way to get the single entity with pedestrian pathfinding data
        if (SystemAPI.TryGetSingletonRW<PathfindPedestrianData>(out var componentRef))
        {
            // By using "ref", we get a direct reference to the game's data, not a copy
            ref PathfindPedestrianData pedestrianData = ref componentRef.ValueRW;
            
            // Log the original value before we change it
            log.Info($"Original UnsafeCrosswalkCost 'behaviour' value: {pedestrianData.m_UnsafeCrosswalkCost.m_Value.x}");

            // The PathfindCosts are stored in a vector (behaviour, comfort, time, money).
            // We are changing the 'x' component, which corresponds to the 'behaviour' cost.
            pedestrianData.m_UnsafeCrosswalkCost.m_Value.x = 500f;

            log.Info($"New UnsafeCrosswalkCost 'behaviour' value set to: {pedestrianData.m_UnsafeCrosswalkCost.m_Value.x}");
        }
        else
        {
            log.Warn("Could not find PathfindPedestrianData singleton to modify.");
        }
    }
}
