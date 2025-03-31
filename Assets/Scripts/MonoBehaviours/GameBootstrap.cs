using Unity.Entities;
using UnityEngine;

namespace CubeSwarm.MonoBehaviours
{
    public class GameBootstrap : MonoBehaviour
    {
        void Start()
        {
            // Ensure the default world is created
            var world = World.DefaultGameObjectInjectionWorld;
            if (world == null)
            {
                Debug.LogError("Default world not created!");
                return;
            }

            // Log systems for debugging
            var systems = world.Systems;
            foreach (var system in systems)
            {
                Debug.Log($"System registered: {system.GetType().Name}");
            }
        }
    }
} 