using System.Collections.Generic;

public interface IPlanetarySystem
{
    IEnumerable<IPlanetaryObject> PlanetaryObjects { get; }

    void Update(float deltaTime);
}
