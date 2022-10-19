using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Transforms;
using Random = Unity.Mathematics.Random;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class RandomSystem : SystemBase
{
	public NativeArray<Random> RandomArray { get; private set; }

	protected override void OnCreate()
	{
		var threads = JobsUtility.MaxJobThreadCount;

		var randomArray = new Random[threads];

		var seed = new System.Random();

		for (int i = 0; i < threads; i++)
		{
			randomArray[i] = new Random((uint)seed.Next());
		}

		RandomArray = new NativeArray<Random>(randomArray, Allocator.Persistent);
	}

	protected override void OnDestroy()
	{
		RandomArray.Dispose();
	}

	protected override void OnUpdate() { }
}
