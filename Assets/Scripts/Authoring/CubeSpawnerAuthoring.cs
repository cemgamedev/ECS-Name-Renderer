using CubeSwarm.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace CubeSwarm.Authoring
{
    public class CubeSpawnerAuthoring : MonoBehaviour
    {
        public GameObject CubePrefab;
        public int NumberOfCubes = 10000;
        public float SpawnRadius = 10f;
        public float MinSpeed = 1f;
        public float MaxSpeed = 3f;
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 3f;
        public string ShapeText = "CEM";
        public float LetterSpacing = 3f;
        public float LetterScale = 1f;
        public Vector3 TargetPosition = Vector3.zero;

        public class Baker : Baker<CubeSpawnerAuthoring>
        {
            public override void Bake(CubeSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                Debug.Log($"Baking CubeSpawner with text: {authoring.ShapeText}");
                
                AddComponent(entity, new CubeSpawnerData
                {
                    Prefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic),
                    NumberOfCubes = authoring.NumberOfCubes,
                    SpawnRadius = authoring.SpawnRadius,
                    MinSpeed = authoring.MinSpeed,
                    MaxSpeed = authoring.MaxSpeed,
                    MinRotationSpeed = authoring.MinRotationSpeed,
                    MaxRotationSpeed = authoring.MaxRotationSpeed,
                    ShapeText = new Unity.Collections.FixedString32Bytes(authoring.ShapeText),
                    LetterSpacing = authoring.LetterSpacing,
                    LetterScale = authoring.LetterScale,
                    TargetPosition = authoring.TargetPosition
                });
            }
        }
    }
} 