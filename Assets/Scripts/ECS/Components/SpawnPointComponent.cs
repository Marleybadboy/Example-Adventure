
using Unity.Entities;
using Unity.Mathematics;

namespace HCC.EnemySystem
{
    public struct SpawnPointComponent : IComponentData
    {
        public float3 _SpawnPosition;
    }
}
