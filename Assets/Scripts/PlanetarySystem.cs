using System.Collections.Generic;

public class PlanetarySystem : IPlanetarySystem // also can be implemented IUpdatable
{
    public IEnumerable<IPlanetaryObject> PlanetaryObjects => _planetaryObjects;

    private readonly IEnumerable<PlanetaryObject> _planetaryObjects;
    private readonly float _simulationSpeed = 15f;

    public PlanetarySystem(IEnumerable<PlanetaryObject> planetaryObjects) 
        => _planetaryObjects = planetaryObjects;

    public void Update(float deltaTime)
    {
        foreach (var obj in _planetaryObjects)
            obj.Rotate(_simulationSpeed * deltaTime);
    }
}
