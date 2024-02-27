

using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;

namespace HCC.EnemySystem
{
    [BurstCompile]
    public partial struct EnemySystem : ISystem
    {

        public void OnCreate(ref SystemState state) 
        {
            //state.RequireForUpdate<SpawnPointComponent>();
           
        }
        public void OnUpdate(ref SystemState state) 
        {
           /* state.Enabled = false;
            var ecb = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var enemySpawner = SystemAPI.GetSingletonEntity<EnemySpawnerComponent>();
            var enemyaspect = SystemAPI.GetAspect<EnemySpawnAspect>(enemySpawner);
            RefRW<RandomComponent> random = SystemAPI.GetSingletonRW<RandomComponent>();*/
            //RefRW<SpawnPointComponent> spawnpoint = SystemAPI.GetSingletonRW<SpawnPointComponent>();

          
            
          


        }
    }
    [BurstCompile]
    public partial struct SpawnJob: IJobEntity 
    {
        public EntityCommandBuffer buffer;
        public int SpawnCount;
        [NativeDisableUnsafePtrRestriction] public EnemySpawnAspect SpawnAspect;

        
        public void Execute(SpawnPointAspect spawnPoint) 
        {
            spawnPoint.SpawnEnemy(buffer, SpawnCount, SpawnAspect);
        
        }
    
    
    }
}
