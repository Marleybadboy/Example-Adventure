
using HCC.EnemySystem;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

public partial class SpawnSystem : SystemBase
{
    protected override void OnUpdate()
    {
     /*  EntityQuery spawnpointquery = EntityManager.CreateEntityQuery(typeof(SpawnPointComponent));
       EntityQuery enemy = EntityManager.CreateEntityQuery(typeof(EnemyComponent));
       var sp = spawnpointquery.ToEntityArray(Allocator.TempJob);
       EntityCommandBuffer ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
       EnemySpawnerComponent spawner = SystemAPI.GetSingleton<EnemySpawnerComponent>();
        int spawncount = 1;

        if (enemy.CalculateEntityCount() <= spawncount)
        {
            foreach (var entity in sp)
            {
                var spawnpoint = SystemAPI.GetComponent<SpawnPointComponent>(entity);
                Entity enemyspawned = ecb.Instantiate(spawner._EnemyPrefab);
                ecb.SetComponent(enemyspawned, LocalTransform.FromPosition(spawnpoint._SpawnPosition));
                ecb.AddComponent(enemyspawned, new EnemyComponent());
            }
        }*/
        

    }
}
