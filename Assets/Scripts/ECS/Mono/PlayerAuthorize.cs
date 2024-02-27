
using Unity.Entities;
using UnityEngine;


namespace HCC.Player
{
    public class PlayerAuthorize : MonoBehaviour
    {
        public float Speed;
    }

    public class PlayerBaker : Baker<PlayerAuthorize>
    {
        public override void Bake(PlayerAuthorize authoring)
        {
            var enity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(enity,new PlayerComponent());
            AddComponent(enity,new PlayerSpeed { Value = authoring.Speed });
            AddComponent(enity, new PlayerTag());
           

        }
    }
}
