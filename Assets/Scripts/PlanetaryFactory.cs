//using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static IPlanetaryObject;

public class PlanetaryFactory : MonoBehaviour, IPlanetaryFactory
{
    [SerializeField] private PlanetaryObject _planetPrefab;
    [SerializeField] private int _minPlanetsCount;
    [SerializeField] private int _maxPlanetsCount;
    [SerializeField] private float _maxPlanetMass;
    [SerializeField] private float _maxPlanetRadius;
    [SerializeField] private float _minGap;
    [SerializeField] private float _maxGap;
    [SerializeField] private float _initialGap;
    [SerializeField] private List<PlanetType> planetTypes;

    public IPlanetarySystem Create(double mass)
    {
        int planetsCount = Random.Range(_minPlanetsCount, _maxPlanetsCount + 1);
        double currentSystemRadius = _initialGap;
        double currentSystemMass = 0;
        PlanetaryObject[] planetaryObjects = new PlanetaryObject[planetsCount];
        double[] masses = GenerateMasses(planetsCount, out double totalMass);
        double massFactor = mass / totalMass;

        for (int i = 0; i < planetsCount; i++)
        {
            double planetMass = masses[i] * massFactor < _maxPlanetMass ? masses[i] * massFactor : _maxPlanetMass;
            currentSystemMass += planetMass;
            PlanetaryObject planetaryObject = GetPlanetFromMass(planetMass, currentSystemRadius);
            float gap = Random.Range(_minGap, _maxGap);
            currentSystemRadius += 2 * planetaryObject.Radius + gap;
            planetaryObjects[i] = planetaryObject;
        }
        var planetarySystem = new PlanetarySystem(planetaryObjects);

        return planetarySystem;
    }

    private double[] GenerateMasses(int planetsCount, out double totalMass)
    {
        double[] masses = new double[planetsCount];
        totalMass = 0;
        for (int i = 0; i < masses.Length; i++)
        {
            var multiplier = Random.Range(0f, 1f);
            multiplier = Mathf.Pow(multiplier, 4);
            masses[i] = multiplier * _maxPlanetMass;
            totalMass += masses[i];
        }
        return masses;
    }

    private PlanetaryObject GetPlanetFromMass(double mass, double currentSystemRadius)
    {
        PlanetaryObject planetaryObject = Instantiate(_planetPrefab, Vector3.zero, Quaternion.identity);
        PlanetType planetType = TypeFromMass(mass);
        double radius = mass.Remap(planetType.MinMass, planetType.MaxMass, planetType.MinRadius, planetType.MaxRadius);
        planetaryObject.Init(mass, (float)radius, (float)(currentSystemRadius + radius), transform.position, planetType.Color);
        return planetaryObject;
    }

    private PlanetType TypeFromMass(double mass)
    {
        foreach (var type in planetTypes)
        {
            if (mass < type.MaxMass)
                return type;
        }

        return planetTypes.Last();
    }

    [System.Serializable]
    public struct PlanetType
    {
        [SerializeField] private MassClassEnum _massClassEnum;
        [SerializeField] private float _minMass;
        [SerializeField] private float _maxMass;
        [SerializeField] private float _minRadius;
        [SerializeField] private float _maxRadius;
        [SerializeField] private Color _color;

        public readonly MassClassEnum MassClassEnum => _massClassEnum;
        public readonly float MinMass => _minMass;
        public readonly float MaxMass => _maxMass;
        public readonly float MinRadius => _minRadius;
        public readonly float MaxRadius => _maxRadius;
        public readonly Color Color => _color;
    }
}