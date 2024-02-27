
using Unity.Entities;

namespace HCC.EnemySystem
{
    public struct EnemySpawnerComponent : IComponentData
    {
        public Entity _EnemyPrefab;
        public int _EnemySpawnObjects;
    }
}
