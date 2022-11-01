using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct WorldMousePosition : IComponentData
{
	public float3 Value;
}
