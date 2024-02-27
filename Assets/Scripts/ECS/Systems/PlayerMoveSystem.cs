

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HCC.Player
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob {deltaTime = deltaTime}.Schedule();
        }
    }

    public partial struct PlayerMoveJob : IJobEntity 
    {
        public float deltaTime;
        private void Execute(ref LocalTransform tranform, in PlayerComponent playerinput, PlayerSpeed speed) 
        {
            tranform.Position.xy += playerinput.Value * speed.Value * deltaTime;

            if(math.lengthsq(playerinput.Value.x) > float.Epsilon ) 
            {
                tranform.Rotation = 0f > playerinput.Value.x  ? quaternion.RotateY(3.14f) : quaternion.RotateY(0f);
            }
           
        }
    }
    
}

