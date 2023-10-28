using UnityEngine;

public class Starter : MonoBehaviour
{
    [SerializeField] private PlanetaryFactory _planetaryFactory;
    [SerializeField] private Updater _updater;

    private void Awake()
    {
        IPlanetarySystem planetarySystem = _planetaryFactory.Create(GlobalSettings.SYSTEM_MASS);
        _updater.Add(planetarySystem);
    }
}
