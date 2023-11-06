using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public partial struct BasketMovementSystem : ISystem
{
    [BurstCompile]

    public void OnUpdate(ref SystemState state)
    {
        foreach (var (transform, properties) in SystemAPI.Query<RefRW<LocalTransform>, RefRW<BasketProperties>>())
        {
            var pos = transform.ValueRO.Position;
            var speed = properties.ValueRO.Speed;

            //Get the mouse's world position
            var mousePos3D = GetMouseWorldPosition();

            //moves the basket entity to match the mouse pointer
            pos.x = mousePos3D.x;

            //Update the basket's position
            transform.ValueRW.Position = pos;
        }

    }

    private float3 GetMouseWorldPosition()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos2D);
    }

}

