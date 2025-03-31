using Unity.Entities;

namespace CubeSwarm.Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class CubeSystemGroup : ComponentSystemGroup { }
} 