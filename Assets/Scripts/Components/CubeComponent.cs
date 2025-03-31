using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace CubeSwarm.Components
{
    public struct CubeComponent : IComponentData
    {
        public float3 CenterPosition;  // Küpün merkez pozisyonu
        public float3 TargetPosition;  // Küpün hedef pozisyonu
    }
} 