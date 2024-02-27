
using Unity.Entities;
using Unity.Mathematics;

namespace HCC.EnemySystem
{
    public readonly partial struct EnemySpawnAspect : IAspect
    {
        public readonly Entity entity;
        private readonly RefRW<EnemySpawnerComponent> _component;
        //private readonly RefRW<SpawnPointComponent> _spawnPointComponent;
        readonly RefRW<RandomComponent> _randomcomponent;

        public RefRW<RandomComponent> m_randomcomponent => _randomcomponent;
        public RefRW<EnemySpawnerComponent> m_spawnercomponent => _component;
        public float3 GetRandomPosition(RefRW<RandomComponent> random, SpawnPointComponent[] spawnpoint) 
        {
            int indexrandom = random.ValueRW.Value.NextInt(0, spawnpoint.Length -1);
            return spawnpoint[indexrandom]._SpawnPosition;
        
        }
    }
}
