
using HCC.EnemySystem;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace HCC.EnemySystem
{
    public readonly partial struct SpawnPointAspect : IAspect
    {
        public readonly Entity _entity;
        private readonly RefRO<SpawnPointComponent> _componentSpawnPoint;

        public float3 m_spawnposition => _componentSpawnPoint.ValueRO._SpawnPosition;

        public void SpawnEnemy(EntityCommandBuffer buffer, int spawnobject, EnemySpawnAspect spawnaspect) 
        { 
            for(int i = 0; i < spawnobject; i++) 
            {
                Entity entity = buffer.Instantiate(spawnaspect.m_spawnercomponent.ValueRW._EnemyPrefab);
                buffer.AddComponent(entity, new EnemyComponent());
                buffer.SetComponent(entity, LocalTransform.FromPosition(m_spawnposition));
            
            }
        
        }
    }
}
