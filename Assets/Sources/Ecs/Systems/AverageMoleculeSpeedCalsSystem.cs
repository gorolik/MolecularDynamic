using Leopotam.Ecs;
using Sources.Ecs.Components;

/// <summary>
/// —читает среднюю скорость всех молекул и записывает их в Simulation Data
/// </summary>
public class AverageMoleculeSpeedCalsSystem : IEcsRunSystem
{
    private readonly EcsFilter<MomentumComponent> _translationFilter = null;
    private readonly SimulationData _simulationData = null;

    public void Run()
    {
        long count = 0;
        double speedSum = 0;

        foreach (int i in _translationFilter)
        {
            ref var momentumComponent = ref _translationFilter.Get1(i);
            ref var momentum = ref momentumComponent.Momentum;

            speedSum += momentum.magnitude;
            count++;
        }

        _simulationData.AverageParticlesMovementSpeed = (float)(speedSum / count);
    }
}
