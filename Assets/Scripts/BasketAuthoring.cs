using Unity.Entities;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class BasketAuthoring : MonoBehaviour
{
    public float3 Position;
    public float Speed;

    private class BasketBaker : Baker<BasketAuthoring>
    {
        public override void Bake(BasketAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            var propertiesComponent = new BasketProperties
            {
                Position = authoring.Position,
                Speed = authoring.Speed,
            };

            AddComponent(entity, propertiesComponent);
        }
    }
}

public struct BasketProperties : IComponentData
{
    public float3 Position;
    public float Speed;
}