using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using CubeSwarm.Components;

namespace CubeSwarm.Systems
{
    [UpdateInGroup(typeof(CubeSystemGroup))]
    public partial struct CubeSpawnSystem : ISystem
    {
        private EntityQuery spawnerQuery;
        private bool hasSpawned;

        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<CubeSpawnerData>();
            spawnerQuery = state.GetEntityQuery(ComponentType.ReadOnly<CubeSpawnerData>());
            hasSpawned = false;
            Debug.Log("CubeSpawnSystem created");
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            if (hasSpawned) return;

            if (spawnerQuery.IsEmpty)
            {
                Debug.LogWarning("No spawner entity found!");
                return;
            }

            var spawnerEntity = spawnerQuery.GetSingletonEntity();
            var spawnerData = state.EntityManager.GetComponentData<CubeSpawnerData>(spawnerEntity);

            if (spawnerData.Prefab == Entity.Null)
            {
                Debug.LogError("Cube prefab is null!");
                return;
            }

            Debug.Log($"Starting spawn process with text: {spawnerData.ShapeText}");

            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            var random = new Unity.Mathematics.Random(1234);

            // Text işleme
            var text = spawnerData.ShapeText;
            int textLength = text.Length;
            Debug.Log($"Text length: {textLength}");

            // Grid boyutları
            const int GRID_ROWS = 7;
            const int GRID_COLS = 5;
            const float CUBE_SPACING = 1.5f;

            // Her harf için aktif hücre sayısını hesapla
            int totalActiveCells = 0;
            for (int letterIndex = 0; letterIndex < textLength; letterIndex++)
            {
                byte currentLetter = text[letterIndex];
                for (int row = 0; row < GRID_ROWS; row++)
                {
                    for (int col = 0; col < GRID_COLS; col++)
                    {
                        if (ShouldPlaceCube((char)currentLetter, row, col, GRID_ROWS, GRID_COLS))
                        {
                            totalActiveCells++;
                        }
                    }
                }
            }

            Debug.Log($"Total active cells: {totalActiveCells}");

            // Her aktif hücreye düşen küp sayısını hesapla
            int cubesPerActiveCell = spawnerData.NumberOfCubes / totalActiveCells;
            Debug.Log($"Cubes per active cell: {cubesPerActiveCell}");

            float letterWidth = GRID_COLS * CUBE_SPACING;
            float totalWidth = textLength * (letterWidth + spawnerData.LetterSpacing);
            float startX = -totalWidth / 2;
            float cubeSpread = 0.5f;

            int totalSpawnedCubes = 0;

            for (int letterIndex = 0; letterIndex < textLength; letterIndex++)
            {
                float letterX = startX + letterIndex * (letterWidth + spawnerData.LetterSpacing);
                byte currentLetter = text[letterIndex];
                Debug.Log($"Processing letter: {(char)currentLetter}");

                for (int row = 0; row < GRID_ROWS; row++)
                {
                    for (int col = 0; col < GRID_COLS; col++)
                    {
                        if (ShouldPlaceCube((char)currentLetter, row, col, GRID_ROWS, GRID_COLS))
                        {
                            for (int i = 0; i < cubesPerActiveCell; i++)
                            {
                                var cube = ecb.Instantiate(spawnerData.Prefab);

                                float baseX = letterX + col * CUBE_SPACING;
                                float baseY = (GRID_ROWS/2 - row) * CUBE_SPACING;

                                float offsetX = random.NextFloat(-cubeSpread, cubeSpread);
                                float offsetY = random.NextFloat(-cubeSpread, cubeSpread);
                                float offsetZ = random.NextFloat(-cubeSpread, cubeSpread);

                                float3 position = new float3(
                                    baseX + offsetX,
                                    baseY + offsetY,
                                    offsetZ * 2
                                );

                                ecb.SetComponent(cube, LocalTransform.FromPosition(position));
                                ecb.AddComponent(cube, new CubeComponent
                                {
                                    CenterPosition = position,
                                    TargetPosition = position
                                });

                                totalSpawnedCubes++;
                            }
                        }
                    }
                }
            }

            Debug.Log($"Total cubes spawned: {totalSpawnedCubes}");

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
            hasSpawned = true;
        }

        private static bool ShouldPlaceCube(char letter, int row, int col, int gridRows, int gridCols)
        {
            switch (letter)
            {
                case 'C':
                    return (col == 0 && row > 0 && row < gridRows-1) || // Sol kenar
                           (row == 0 && col > 0 && col < gridCols-1) || // Üst kenar
                           (row == gridRows-1 && col > 0 && col < gridCols-1); // Alt kenar

                case 'E':
                    return (col == 0) || // Sol kenar
                           (row == 0 && col < gridCols-1) || // Üst kenar
                           (row == gridRows-1 && col < gridCols-1) || // Alt kenar
                           (row == gridRows/2 && col < gridCols-1); // Orta çizgi

                case 'M':
                    return (col == 0) || // Sol kenar
                           (col == gridCols-1) || // Sağ kenar
                           (row <= gridRows/2 && (row == col || col == gridCols-1-row)); // Çaprazlar

                default:
                    return false;
            }
        }
    }
} 