using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using CubeSwarm.Components;

namespace CubeSwarm.Systems
{
    [UpdateInGroup(typeof(CubeSystemGroup))]
    [UpdateAfter(typeof(CubeSpawnSystem))]
    public partial struct CubeMovementSystem : ISystem
    {
        private float elapsedTime;

        public void OnCreate(ref SystemState state)
        {
            elapsedTime = 0f;
            state.RequireForUpdate<CubeComponent>();
        }

        public void OnDestroy(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            elapsedTime += deltaTime;
            
            float currentTime = elapsedTime;
            const float speed = 0.5f;
            const float range = 0.3f;

            foreach (var (transform, cube) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<CubeComponent>>())
            {
                float3 basePosition = cube.ValueRO.CenterPosition;
                float offset = math.sin(currentTime * speed + basePosition.x + basePosition.z) * range;
                transform.ValueRW.Position = new float3(
                    basePosition.x,
                    basePosition.y + offset,
                    basePosition.z
                );
            }
        }
    }
} 