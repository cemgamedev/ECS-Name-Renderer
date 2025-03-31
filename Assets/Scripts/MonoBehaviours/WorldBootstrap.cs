using Unity.Entities;
using UnityEngine;
using Unity.Scenes;
using CubeSwarm.Systems;

[DefaultExecutionOrder(-1)]
public class WorldBootstrap : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("WorldBootstrap Awake");
        var world = World.DefaultGameObjectInjectionWorld;
        if (world == null)
        {
            Debug.LogError("Default world not found!");
            return;
        }

        // Add our system group
        var systemGroup = world.GetOrCreateSystemManaged<CubeSystemGroup>();
        var simGroup = world.GetOrCreateSystemManaged<SimulationSystemGroup>();
        simGroup.AddSystemToUpdateList(systemGroup);

        Debug.Log("Added CubeSystemGroup to simulation group");
    }

    private void Start()
    {
        Debug.Log("WorldBootstrap Start");
        var world = World.DefaultGameObjectInjectionWorld;
        
        // Log all available systems
        foreach (var system in world.Systems)
        {
            Debug.Log($"System available: {system.GetType().Name}");
        }
    }

    private void OnEnable()
    {
        Debug.Log("WorldBootstrap Enabled");
    }
} 