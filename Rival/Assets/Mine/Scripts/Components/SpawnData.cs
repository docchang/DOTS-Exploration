using Unity.Entities;
using Unity.Mathematics;

public struct SpawnData : IComponentData
{
	public float3 gridSize;
	public int spread;
}