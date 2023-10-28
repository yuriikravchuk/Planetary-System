using System;
using Unity.VisualScripting;
using UnityEngine;
using static IPlanetaryObject;

[RequireComponent(typeof(MeshRenderer))]
public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
{
    public MassClassEnum MassClass { get; private set; }
    public double Mass { get; private set; }
    public double Radius { get; private set; }
    public float Distance { get; private set; }

    private Transform _transform;
    private MeshRenderer _meshRenderer;
    private Transform _rotationPoint;
    private float _angularSpeed;

    public void Init(double mass, float radius, float distance, Vector3 position, Color color)
    {
        _transform = transform;
        _meshRenderer = GetComponent<MeshRenderer>();

        _angularSpeed = Mathf.Sqrt((GlobalSettings.GRAVITY * GlobalSettings.SUN_MASS) / (Mathf.Pow(distance, 3)));
        Mass = mass;
        Radius = radius;
        Distance = distance;
        transform.localScale = Vector3.one * radius;
        Material material = new Material(_meshRenderer.sharedMaterial);
        material.SetColor("_Color", color);
        _meshRenderer.sharedMaterial = material;
        _rotationPoint = CreateRotationPoint(position);
        _transform.localPosition = Vector3.forward * Distance;
    }

    private Transform CreateRotationPoint(Vector3 position)
    {
        var centerPointTransform = new GameObject("Pivot").transform;
        centerPointTransform.position = position;
        _transform.SetParent(centerPointTransform);
        return centerPointTransform;
    }

    public void Rotate(float speed) => _rotationPoint.Rotate(0, speed * _angularSpeed, 0);
}
