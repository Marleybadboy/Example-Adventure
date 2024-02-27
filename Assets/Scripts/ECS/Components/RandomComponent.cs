
using Unity.Entities;
using Unity.Mathematics;

namespace HCC.EnemySystem
{
    public struct RandomComponent : IComponentData
    {
        public Random Value;
    }
}
