
using Unity.Entities;
using UnityEngine;

namespace HCC.EnemySystem
{
    public class EnemySpawnerAuthorize : MonoBehaviour
    {
        public GameObject _EnemyPrefab;
        public int _ValueSpawn;

    }

    public class EnemySpawnerBaker : Baker<EnemySpawnerAuthorize>
    {
        public override void Bake(EnemySpawnerAuthorize authoring)
        {
            var entityPrefab = GetEntity(authoring._EnemyPrefab, TransformUsageFlags.Dynamic);
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemySpawnerComponent
            {
                _EnemyPrefab = entityPrefab,
                _EnemySpawnObjects = authoring._ValueSpawn

            });
            AddComponent(entity, new RandomComponent
            {
                Value = new Unity.Mathematics.Random(1)
            }) ;
        }
    }
}
