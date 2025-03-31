using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CubeSwarm.MonoBehaviours
{
    public class CubeSpawnerAuthoring : MonoBehaviour
    {
        public GameObject CubePrefab;
        public int NumberOfCubes = 50000;
        public float SpawnRadius = 10f;
        public float MinSpeed = 2f;
        public float MaxSpeed = 5f;
        public float MinRotationSpeed = 1f;
        public float MaxRotationSpeed = 3f;
        public string ShapeText = "CEM"; // Şekil olarak çizilecek metin
        public float LetterSpacing = 3f;   // Harfler arası boşluk
        public float LetterScale = 1f;     // Harf boyutu
        public float3 TargetPosition;

        private void OnEnable()
        {
            Debug.Log($"CubeSpawnerAuthoring enabled with {NumberOfCubes} cubes");
            if (CubePrefab == null)
            {
                Debug.LogError("Cube Prefab is not assigned!");
            }
        }
    }

    public class CubeSpawnerBaker : Baker<CubeSpawnerAuthoring>
    {
        public override void Bake(CubeSpawnerAuthoring authoring)
        {
            Debug.Log("Baking CubeSpawner");
            
            if (authoring.CubePrefab == null)
            {
                Debug.LogError("Cannot bake CubeSpawner: Cube Prefab is null!");
                return;
            }

            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new CubeSpawnerData
            {
                Prefab = GetEntity(authoring.CubePrefab, TransformUsageFlags.Dynamic),
                NumberOfCubes = authoring.NumberOfCubes,
                SpawnRadius = authoring.SpawnRadius,
                MinSpeed = authoring.MinSpeed,
                MaxSpeed = authoring.MaxSpeed,
                MinRotationSpeed = authoring.MinRotationSpeed,
                MaxRotationSpeed = authoring.MaxRotationSpeed,
                ShapeText = authoring.ShapeText,
                LetterSpacing = authoring.LetterSpacing,
                LetterScale = authoring.LetterScale,
                TargetPosition = authoring.TargetPosition
            });

            Debug.Log("CubeSpawner baking completed");
        }
    }

    public struct CubeSpawnerData : IComponentData
    {
        public Entity Prefab;
        public int NumberOfCubes;
        public float SpawnRadius;
        public float MinSpeed;
        public float MaxSpeed;
        public float MinRotationSpeed;
        public float MaxRotationSpeed;
        public FixedString64Bytes ShapeText; // Unity'nin fixed string tipi
        public float LetterSpacing;
        public float LetterScale;
        public float3 TargetPosition;
    }
} 