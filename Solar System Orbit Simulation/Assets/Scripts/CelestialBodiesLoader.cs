using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class CelestialBodiesLoader : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject moonPrefab;
    public GameObject starPrefab;
    public Sun sun;
    public Vector3 NextStarPosition = new(0f, 0f, 0f);
    private CelestialBodies celestialBodies;
    void Start()
    {
        string fileName = Application.dataPath + "/celestial_bodies.json";
        string jsonContent = File.ReadAllText(fileName);

        celestialBodies = JsonConvert.DeserializeObject<CelestialBodies>(jsonContent);

        LoadSunAndPlanets();
    }

    public void LoadSunAndPlanets()
    {
        foreach (var star in celestialBodies.Stars)
        {
            GameObject starOwnPrefab = Resources.Load<GameObject>(star.name);
            GameObject starGameObject;
            if (starOwnPrefab != null)
            {
                starGameObject = Instantiate(starOwnPrefab);
            }
            else
            {
                starGameObject = Instantiate(starPrefab);
            }

            Sun starComponent = starGameObject.GetComponent<Sun>();
            SphereCollider starCollider = starGameObject.GetComponent<SphereCollider>();

            StarEntity starEntity = star;
            starComponent.Id = starEntity.id;
            starComponent.name = starEntity.name;
            starComponent.Mass = starEntity.mass;
            starGameObject.transform.localScale= new(1,1,1);
            starCollider.radius = 1f;
            sun = starComponent;
            starGameObject.transform.position = NextStarPosition;

            List<PlanetEntity> planetList = celestialBodies.Planets.Where(planet => planet.aroundObjectId == star.id).OrderBy(planet => planet.meanRadius).ToList();
            Vector3 scale = new(0.1f, 0.1f, 0.1f);
            scale *= starGameObject.transform.localScale.x;
            foreach (var planet in planetList)
            {
                GameObject planetOwnPrefab = Resources.Load<GameObject>(planet.name);
                GameObject planetGameObject;
                if (planetOwnPrefab != null)
                {
                    planetGameObject = Instantiate(planetOwnPrefab);
                }
                else
                {
                    planetGameObject = Instantiate(planetPrefab);
                }

                Planet planetComponent = planetGameObject.GetComponent<Planet>();
                SphereCollider planetCollider = planetGameObject.GetComponent<SphereCollider>();

                planetComponent.sun = starComponent;
                planetComponent.sunTransform = starGameObject.transform;
                planetComponent.Id = planet.id;
                planetComponent.name = planet.name;
                planetComponent.Mass = planet.mass;
                planetComponent.MeanRadius = planet.meanRadius;
                planetComponent.DistanceReal = new Vector3(planet.perihelion * 1000f, 0f, 0f) + starGameObject.transform.position;
                planetComponent.InclinationDegrees = planet.inclination;
                planetComponent.CalculateStartingValues(scale);
                planetCollider.radius = scale.x * starGameObject.transform.localScale.x + 0.2f;
                scale += new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
        NextStarPosition += new Vector3(0f, 1000f, 0f);
        NextStarPosition = new Vector3(0f, 0f, 0f);
    }

    public void LoadMoonsOfPlanet(GameObject planetObject)
    {
        Planet planet = planetObject.GetComponent<Planet>();
        List<MoonEntity> moonsList = celestialBodies.Moons.Where(moon => moon.aroundObjectId == planet.Id).OrderBy(moon => moon.perigee).ToList();
        if (moonsList.Count != 0)
        {
            Vector3 scale = new(0.5f, 0.5f, 0.5f);
            planet.lunarDistance = moonsList.First().perigee * 1000;
            planet.dt = 50;
            if (planet.name == "Mars")
            {
                planet.dt = 0.25f;
            }

            foreach (var moon in moonsList)
            {
                GameObject moonObject = Instantiate(moonPrefab, planetObject.transform);
                Moon moonComponent = moonObject.GetComponent<Moon>();
                SphereCollider moonCollider = moonObject.GetComponent<SphereCollider>();

                moonComponent.planet = planet;
                moonComponent.Id = moon.id;
                moonComponent.name = moon.name;
                moonComponent.Mass = moon.mass;
                moonComponent.MeanRadius = moon.meanRadius;
                moonComponent.DistanceReal = new Vector3(moon.perigee * 1000f, 0f, 0f);
                moonComponent.InclinationDegrees = moon.inclination;
                moonComponent.CalculateStartingValues(scale);
                moonCollider.radius = scale.x * planetObject.transform.localScale.x + 0.5f;
            }
        }
    }
}