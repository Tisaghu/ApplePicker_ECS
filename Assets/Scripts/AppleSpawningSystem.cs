using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct AppleSpawningSystem : ISystem
{
    [BurstCompile]

    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<AppleProperties>>())
        {
            // Generate a random time interval for apple spawning
            float spawnInterval = UnityEngine.Random.Range(1.0f, 5.0f); // Adjust the range as needed

            // Check if it's time to spawn a new apple
            if (UnityEngine.Time.time % spawnInterval < UnityEngine.Time.deltaTime)
            {
                // Get the position of the tree from the 'transform' variable
                float3 treePosition = transform.ValueRO.Position;

                // Spawn an apple at the tree's position
                SpawnApple(treePosition);
            }
        }
    }

    private void SpawnApple(float3 spawnPosition)
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        Entity appleEntity = entityManager.CreateEntity();

        entityManager.AddComponentData(appleEntity, new AppleProperties
        {
            Position = spawnPosition,
            FallSpeed = 5.0f, // Set the fall speed as needed
            IsCaught = false // Set the initial state
        });

        // Add other components if needed

        // Instantiate the visual representation of the apple and set its position manually
        GameObject applePrefab = Resources.Load<GameObject>("ApplePrefab"); // Replace with your apple prefab's name
        GameObject appleGameObject = Object.Instantiate(applePrefab);
        appleGameObject.transform.position = spawnPosition;
    }

}
