using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

namespace CubeSwarm.Components
{
    public struct CubeSpawnerData : IComponentData
    {
        public Entity Prefab;
        public int NumberOfCubes;
        public float SpawnRadius;
        public float MinSpeed;
        public float MaxSpeed;
        public float MinRotationSpeed;
        public float MaxRotationSpeed;
        public FixedString32Bytes ShapeText;
        public float LetterSpacing;
        public float LetterScale;
        public float3 TargetPosition;
    }
} 