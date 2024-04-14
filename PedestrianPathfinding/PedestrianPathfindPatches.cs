using Game.Pathfind;
using Game.Prefabs;
using HarmonyLib;
using Unity.Entities;

namespace PedestrianPathfinding;

[HarmonyPatch]
public class PedestrianPathfindPatches
{
    private static PathfindCosts m_WalkingCost = new PathfindCosts(time: 0f, behaviour: 0f, money: 0f, comfort: 0.01f);
    private static PathfindCosts m_CrosswalkCost = new PathfindCosts(time: 0f, behaviour: 0f, money: 0f, comfort: 5f);
    private static PathfindCosts m_UnsafeCrosswalkCost = new PathfindCosts(time: 0f, behaviour: 1000f, money: 0f, comfort: 5f);
    private static PathfindCosts m_SpawnCost = new PathfindCosts(time: 5f, behaviour: 0f, money: 0f, comfort: 0f);

    [HarmonyPatch(typeof(PedestrianPathfind), nameof(PedestrianPathfind.Initialize))]
    [HarmonyPrefix]
    public static bool Prefix(ref EntityManager entityManager, ref Entity entity)
    {
        PathfindPedestrianData componentData = default;
        componentData.m_WalkingCost = m_WalkingCost;
        componentData.m_CrosswalkCost = m_CrosswalkCost;
        componentData.m_UnsafeCrosswalkCost = m_UnsafeCrosswalkCost;
        componentData.m_SpawnCost = m_SpawnCost;
        entityManager.SetComponentData(entity, componentData);

        return false;
    }
}