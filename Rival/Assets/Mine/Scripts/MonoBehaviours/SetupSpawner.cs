using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using TMG.ECSPrefabs;

public class SetupSpawner : MonoBehaviour, IConvertGameObjectToEntity
{
	[SerializeField] private GameObject personPrefab;
	[SerializeField] public Vector3 gridSize;
	[SerializeField] public int spread;
	[SerializeField] private Vector2 speedRange = new Vector2(4, 7);

	public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
	{
		var spawnData = new SpawnData
		{
			gridSize = gridSize,
			spread = spread,
		};
		dstManager.AddComponentData(entity, spawnData);
	}

	BlobAssetStore blob;

	void Start()
    {
		// Blob is memory allocation
		blob = new BlobAssetStore();

		// Grab the settings of the default world
		var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blob);

		// Convert prefab to entity
		var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(personPrefab, settings);

		// grab the entity manager and Instantiate it
		var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

		for (int x = 0; x < gridSize.x; x++)
		{
			//for (int y = 0; y < gridSize.y; y++)
			//{
				for (int z = 0; z < gridSize.z; z++)
				{
					var instance = entityManager.Instantiate(entity);

					float3 position = new float3(x * spread, 0, z * spread);

					entityManager.SetComponentData<Translation>(instance, new Translation { Value = position });

					entityManager.SetComponentData<Destinaion>(instance, new Destinaion { Value = position });

					float speed = UnityEngine.Random.Range(speedRange.x, speedRange.y);
					entityManager.SetComponentData<MovementSpeed>(instance, new MovementSpeed { Value = speed });
				}
			//}
		}
    }

	void OnDestroy()
	{
		// dispose memory allocation when we are done
		blob.Dispose();
	}
}
