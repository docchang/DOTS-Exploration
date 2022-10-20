using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.SpaceShooter
{
    [UpdateAfter(typeof(GetPlayerInputSystem))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial class MovePlayerShipSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Dependency = new MovePlayerShipJob
            {
                DeltaTime = Time.DeltaTime,
            }.ScheduleParallel(Dependency);
        }
    }

    [BurstCompile]
    public partial struct MovePlayerShipJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(ref Translation translation, in PlayerMoveInput playerMoveInput, in MoveSpeed moveSpeed)
        {
            var currentMovement = playerMoveInput.Value * moveSpeed.Value * DeltaTime;
            translation.Value += currentMovement;
        }
    }
}
