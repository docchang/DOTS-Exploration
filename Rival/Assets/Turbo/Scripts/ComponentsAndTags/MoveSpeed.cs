using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace TMG.SpaceShooter
{
    [GenerateAuthoringComponent]
    public struct MoveSpeed : IComponentData
    {
        public float Value;
    }
}
