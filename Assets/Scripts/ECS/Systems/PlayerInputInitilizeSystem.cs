

using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace HCC.Player
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputInitilizeSystem : SystemBase
    {
        private Entity _PlayerEntity;
        private PlayerActionsInput _PlayerInput; 

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerComponent>();
            _PlayerInput = new PlayerActionsInput();
        }

        protected override void OnStartRunning()
        {
            _PlayerInput.Enable();
            _PlayerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnStopRunning()
        {
            _PlayerInput.Disable();
            _PlayerEntity = Entity.Null;
        }
        protected override void OnUpdate()
        {
            var curInput = _PlayerInput.PlayerAction.Movment.ReadValue<Vector2>();
            float2 convertinput = new float2(curInput.x, curInput.y);
            SystemAPI.SetSingleton(new PlayerComponent { Value = convertinput});
        }

      
    }
}
