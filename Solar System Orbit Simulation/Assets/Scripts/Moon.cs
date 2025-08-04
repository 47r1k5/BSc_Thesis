using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    //TODO
    //Scaling
    //Calculate grav force for Planet

    public Planet planet;
    public int Id { get; set; }
    public float Mass { get; set; }
    public float MeanRadius { get; set; }
    public Vector3 DistanceReal { get; set; }
    public Vector3 distanceSim;
    public float gravitationalConstantReal;
    public float gravitationalConstantSim;
    public Vector3 VelocityReal { get; set; }
    public Vector3 VelocitySim { get; set; }
    public float InclinationDegrees { get; set; }
    public float distanceScale;

    void FixedUpdate()
    {
        Step();
    }

    void Step()
    {
        Vector3 acceleration = CalculateAcceleration(transform.position, transform.parent.position);
        RK4(planet.dt, acceleration, VelocitySim, transform.parent.position);
    }


    private Vector3 CalculateAcceleration(Vector3 planetPosition, Vector3 starPosition)
    {
        Vector3 distance = planetPosition - starPosition;
        return -gravitationalConstantSim * planet.Mass / distance.sqrMagnitude * distance.normalized;
    }

    public void RK4(float dt, Vector3 acceleration, Vector3 velocity, Vector3 starPosition)
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
        VelocitySim = velocity;
    }

    public void CalculateStartingValues(Vector3 scale)
    {
        gravitationalConstantReal = planet.gravitationalConstantReal;
        VelocityReal = new Vector3(0f, Mathf.Sqrt(gravitationalConstantReal * planet.Mass / DistanceReal.x), 0f);

        Quaternion inclinationRotation = Quaternion.AngleAxis(InclinationDegrees, Vector3.right);
        DistanceReal = inclinationRotation * DistanceReal;
        VelocityReal = inclinationRotation * VelocityReal;

        distanceSim = DistanceReal * (planet.scale / planet.lunarDistance);
        transform.localPosition = distanceSim;
        transform.localScale = scale;
        gravitationalConstantSim = gravitationalConstantReal * Mathf.Pow(planet.scale / planet.lunarDistance, 3);
        VelocitySim = VelocityReal * (planet.scale / planet.lunarDistance);
    }
}
