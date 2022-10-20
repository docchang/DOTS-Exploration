using Unity.Entities;
using Unity.Mathematics;

namespace TMG.SpaceShooter
{
    [GenerateAuthoringComponent]
    public struct PlayerMoveInput : IComponentData
    {
        public float3 Value;
    }
}