using Colossal.Logging;
using Colossal.Serialization.Entities;
using Game;
using Game.Pathfind;
using Game.Prefabs;
using Unity.Entities;

namespace PedestrianPathfinding;

public partial class PedestrianPathfindSystem : GameSystemBase
{
    public override int GetUpdateInterval(SystemUpdatePhase phase)
    {
        return base.GetUpdateInterval(phase);
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
    {
        base.OnGameLoadingComplete(purpose, mode);
        ILog log = LogManager.GetLogger(nameof(PedestrianPathfinding)).SetShowsErrorsInUI(true);
        log.Info($"Loading {purpose}, where mode is {mode}");

        var m_UnsafeCrosswalkCost = new PathfindCosts(time: 0f, behaviour: 500f, money: 0f, comfort: 50f);

        var entities = EntityManager.CreateEntityQuery(ComponentType.ReadWrite<PathfindPedestrianData>()).ToEntityArray(Unity.Collections.Allocator.Persistent);

        foreach (var entity in entities)
        {
            var oldCosts = EntityManager.GetComponentData<PathfindPedestrianData>(entity);

            log.Info($"OnGameLoadingComplete - Setting pedestrian walking costs for prefab, changing from: {oldCosts.m_UnsafeCrosswalkCost.m_Value} to: {m_UnsafeCrosswalkCost.m_Value}");

            PathfindPedestrianData componentData = default;
            componentData.m_WalkingCost = oldCosts.m_WalkingCost;
            componentData.m_CrosswalkCost = oldCosts.m_CrosswalkCost;
            componentData.m_UnsafeCrosswalkCost = m_UnsafeCrosswalkCost;
            componentData.m_SpawnCost = oldCosts.m_SpawnCost;
            EntityManager.SetComponentData(entity, componentData);
        }

        entities.Dispose();
    }
}