
using Unity.Entities;
using Unity.Mathematics;

namespace HCC.Player
{
    public struct PlayerComponent : IComponentData
    {
        public float2 Value;
    }

    public struct PlayerSpeed : IComponentData
    {
        public float Value;
    }
    public struct PlayerTag : IComponentData { }
}
