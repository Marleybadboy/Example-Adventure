
using Unity.Entities;
using UnityEngine;

namespace HCC.EnemySystem
{
    public class SpawnPointAuthorize : MonoBehaviour
    {

    }
    public class SpawnPointBake : Baker<SpawnPointAuthorize>
    {
        public override void Bake(SpawnPointAuthorize authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new SpawnPointComponent 
            { 
                _SpawnPosition = authoring.transform.position,
            });
        }
    }
}
