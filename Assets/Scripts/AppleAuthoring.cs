using Unity.Entities;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class AppleAuthoring : MonoBehaviour
{
    public float3 Position;
    public float FallSpeed;
    public bool IsCaught;
    public GameObject ApplePrefab;
    public uint RandomSeed;

    public class AppleBaker : Baker<AppleAuthoring>
    {
        public override void Bake(AppleAuthoring authoring)
        {
            AddComponent(new AppleProperties
            {
                Position = authoring.Position,
                FallSpeed = authoring.FallSpeed,
                IsCaught = authoring.IsCaught,
                ApplePrefab = GetEntity(authoring.ApplePrefab)
            });
            AddComponent(new AppleRandom
            {
                Value = Unity.Mathematics.Random.CreateFromIndex(authoring.RandomSeed)
            });
        }
    }
}

public struct AppleProperties : IComponentData
{
    public float3 Position;
    public float FallSpeed;
    public bool IsCaught;
    public Entity ApplePrefab;
}

public struct AppleRandom : IComponentData
{
    public Unity.Mathematics.Random Value;
}