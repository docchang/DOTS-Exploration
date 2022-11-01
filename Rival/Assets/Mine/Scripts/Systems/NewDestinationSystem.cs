using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class NewDestinationSystem : SystemBase
{
    private RandomSystem randomSystem;

	private SpawnData spawnData;

	protected override void OnStartRunning()
	{
		//spawnData = GetSingleton<SpawnData>();
	}

	protected override void OnCreate()
	{
        randomSystem = World.GetExistingSystem<RandomSystem>();
	}

	protected override void OnUpdate()
    {
        var randomArray = randomSystem.RandomArray;

		var maxDestValue = spawnData.gridSize * spawnData.spread;

		Entities
			.WithNativeDisableParallelForRestriction(randomArray)
			.ForEach((int nativeThreadIndex, ref Destinaion destinaion, in Translation translation) =>
		{
            float distance = math.abs(math.length(destinaion.Value - translation.Value));

            if (distance < 0.1f)
			{
                var random = randomArray[nativeThreadIndex];

				//destinaion.Value.x = random.NextFloat(0, maxDestValue.x);
				//destinaion.Value.y = random.NextFloat(0, maxDestValue.y);
				//destinaion.Value.z = random.NextFloat(0, maxDestValue.z);

				destinaion.Value.x = random.NextFloat(0, 5 * 50);
				//destinaion.Value.y = random.NextFloat(0, 5 * 5);
				destinaion.Value.z = random.NextFloat(0, 5 * 50);

				randomArray[nativeThreadIndex] = random;
			}
        }).ScheduleParallel();
    }
}
