using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Planet : MonoBehaviour
{
    public Sun sun;
    public Transform sunTransform;
    public int Id { get; set; }
    public float Mass { get; set; }
    public float MeanRadius { get; set; }
    public Vector3 DistanceReal { get; set; }
    public Vector3 distanceSim;
    public float gravitationalConstantReal;
    public float lunarDistance;
    public float gravitationalConstantSim;
    public Vector3 VelocityReal { get; set; }
    public Vector3 velocitySim;
    public float InclinationDegrees { get; set; }
    private float sunMass;
    public float dt;
    public float scale;

    void Start()
    {
        sunMass = sun.Mass;
        dt = sun.dt;
        gravitationalConstantReal = 6.6743e-11f;
        lunarDistance = 362600000f;
        scale = 8;
    }

    void FixedUpdate()
    {
        Step();
    }

    void Step()
    {
        Vector3 starPosition = new(0f, 0f, 0f);
        if (sun != null)
        {
            starPosition = sunTransform.position;
            dt = sun.dt;
        }

        Vector3 acceleration = CalculateAcceleration(transform.position, starPosition);
        RK4(acceleration, velocitySim, starPosition);
    }

    private Vector3 CalculateAcceleration(Vector3 planetPosition, Vector3 starPosition)
    {
        Vector3 distance = planetPosition - starPosition;
        return -gravitationalConstantSim * sunMass / distance.sqrMagnitude * distance.normalized;
    }

    public void CalculateStartingValues(Vector3 scale)
    {
        gravitationalConstantReal = sun.gravitationalConstantReal;
        VelocityReal = new Vector3(0f, Mathf.Sqrt(gravitationalConstantReal * sun.Mass / DistanceReal.x), 0f);

        Quaternion inclinationRotation = Quaternion.AngleAxis(InclinationDegrees, Vector3.right);
        DistanceReal = inclinationRotation * DistanceReal;
        VelocityReal = inclinationRotation * VelocityReal;

        distanceSim = DistanceReal * (sun.scale / sun.astronomicalUnit);
        transform.position = distanceSim;
        transform.localScale = scale;
        gravitationalConstantSim = gravitationalConstantReal * Mathf.Pow(sun.scale / sun.astronomicalUnit, 3);
        velocitySim = VelocityReal * (sun.scale / sun.astronomicalUnit);
    }

    public void RK4(Vector3 acceleration, Vector3 velocity, Vector3 starPosition)
    {
        Vector3 position = transform.position;
        Vector3 k1v = acceleration;
        Vector3 k1r = velocity;

        Vector3 k2v = CalculateAcceleration(position + k1r * dt / 2, starPosition);
        Vector3 k2r = velocity + k1v * dt / 2;

        Vector3 k3v = CalculateAcceleration(position + k2r * dt / 2, starPosition);
        Vector3 k3r = velocity + k2v * dt / 2;

        Vector3 k4v = CalculateAcceleration(position + k3r * dt, starPosition);
        Vector3 k4r = velocity + k3v * dt;

        velocity += (k1v + 2 * k2v + 2 * k3v + k4v) / 6 * dt;
        position += (k1r + 2 * k2r + 2 * k3r + k4r) / 6 * dt;
        transform.position = position;
        velocitySim = velocity;
    }
}
