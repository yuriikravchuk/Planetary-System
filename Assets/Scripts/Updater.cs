using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private readonly List<IPlanetarySystem> _updatables = new List<IPlanetarySystem>();

    private void Update()
    {
        foreach (var updatable in _updatables) 
            updatable.Update(Time.deltaTime);
    }

    public void Add(IPlanetarySystem updatable) => _updatables.Add(updatable);
}
