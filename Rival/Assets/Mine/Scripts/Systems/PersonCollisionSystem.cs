using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Rendering;

public partial class PersonCollisionSystem : SystemBase
{
	private StepPhysicsWorld stepPhysicsWorld;

	protected override void OnCreate()
	{
		stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
	}

	[BurstCompile]
	struct PersonCollisionJob : ITriggerEventsJob
	{
		[ReadOnly]
		public ComponentDataFromEntity<PersonTag> PersonGroup;

		public ComponentDataFromEntity<URPMaterialPropertyBaseColor> ColorGroup;

		public int Seed;

		public void Execute(TriggerEvent triggerEvent)
		{
			bool isEntityAPerson = PersonGroup.HasComponent(triggerEvent.EntityA);
			bool isEntityBPerson = PersonGroup.HasComponent(triggerEvent.EntityB);

			if (!isEntityAPerson || !isEntityBPerson) { return; }

			var random = new Random((uint)((triggerEvent.BodyIndexA * triggerEvent.BodyIndexB) + (1 + Seed)));

			random = ChangeMaterialColor(random, triggerEvent.EntityA);
			random = ChangeMaterialColor(random, triggerEvent.EntityB);
		}

		private Random ChangeMaterialColor(Random random, Entity entity)
		{
			if (ColorGroup.HasComponent(entity))
			{
				var component = ColorGroup[entity];

				component.Value.x = random.NextFloat(0, 1);
				component.Value.y = random.NextFloat(0, 1);
				component.Value.z = random.NextFloat(0, 1);

				ColorGroup[entity] = component;
			}

			return random;
		}
	}

	protected override void OnUpdate()
	{
		Dependency = new PersonCollisionJob
		{
			PersonGroup = GetComponentDataFromEntity<PersonTag>(true),
			ColorGroup = GetComponentDataFromEntity<URPMaterialPropertyBaseColor>(),
			Seed = System.DateTimeOffset.Now.Millisecond,
		}.Schedule(stepPhysicsWorld.Simulation, Dependency);
	}
}
