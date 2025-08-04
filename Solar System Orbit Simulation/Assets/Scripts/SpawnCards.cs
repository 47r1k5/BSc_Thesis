using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCards : MonoBehaviour
{
    private CelestialBodies celestialBodies;
    public GameObject cardPrefab;
    [SerializeField] RectTransform container;
    [SerializeField] RectTransform cards;
    private float fullContainerLength = 0;

    void Start()
    {
        string fileName = Application.dataPath + "/celestial_bodies.json";
        string jsonContent = File.ReadAllText(fileName);
        celestialBodies = JsonConvert.DeserializeObject<CelestialBodies>(jsonContent);
        LoadAllCards();
    }

    public void LoadStarCards()
    {
        float length = celestialBodies.Stars.ToArray().Length;
        fullContainerLength+=length;
        foreach (var star in celestialBodies.Stars)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card cardComponent = cardObject.GetComponent<Card>();
            //cardComponent.ShowButton = cardObject.transform.Find("Show").GetComponent<Button>();
            cardObject.transform.Find("Show").gameObject.SetActive(false);
            cardObject.name=star.name;
            cardComponent.BodyName.SetText(star.name);
            string info = $"Mass: {star.mass} kg\n" +
            $"Volume: {star.vol} km^3\n" +
            $"Mean radius: {star.meanRadius} km";
            cardComponent.Info.SetText(info);
        }
        length = (float)Math.Ceiling(length / 4);

        if (length < 2)
        {
            length = 800;
        }else{
            length = 615 * length + 18;
        }            
        container.sizeDelta = new Vector2(1800, length);
        cards.sizeDelta = new Vector2(1800, length);
        container.localPosition = new Vector2(0, -0.6f*length);
    }
    public void LoadPlanetCards()
    {
        float length = celestialBodies.Planets.ToArray().Length;
        fullContainerLength+=length;
        foreach (var planet in celestialBodies.Planets.OrderBy(p=>p.perihelion))
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card cardComponent = cardObject.GetComponent<Card>();
            cardComponent.BodyName.SetText(planet.name);
            //ardComponent.ShowButton = cardObject.transform.Find("Show").GetComponent<Button>();
            cardObject.transform.Find("Show").gameObject.SetActive(false);
            cardObject.name=planet.name;
            string aroundObjectName = celestialBodies.Stars.First(star => star.id == planet.aroundObjectId).name;

            string info = $"Mass: {planet.mass} kg\n" +
            $"Volume: {planet.vol} km^3\n" +
            $"Mean radius: {planet.meanRadius} km\n" +
            $"Orbits around: {aroundObjectName}\n" +
            $"Semi-major Axis: {planet.semiMajorAxis} km\n" +
            $"Perihelion: {planet.perihelion} km\n" +
            $"Aphelion: {planet.aphelion} km\n" +
            $"Eccentricity: {planet.eccentricity}\n" +
            $"Inclination: {planet.inclination}°";

            cardComponent.Info.SetText(info);
        }
        length = (float)Math.Ceiling(length / 4);

        if (length < 2)
        {
            length = 800;
        }else{
            length = 615 * length + 18;
        }            
        container.sizeDelta = new Vector2(1800, length);
        cards.sizeDelta = new Vector2(1800, length);
        container.localPosition = new Vector2(0, -0.6f*length);
    }
    public void LoadMoonCards()
    {
        float length = celestialBodies.Moons.ToArray().Length;
        fullContainerLength+=length;
        foreach (var moon in celestialBodies.Moons.OrderBy(m=>m.meanRadius).Reverse())
        {
            GameObject cardObject = Instantiate(cardPrefab, transform);
            Card cardComponent = cardObject.GetComponent<Card>();
            cardObject.transform.Find("Show").gameObject.SetActive(false);
            cardObject.name=moon.name;
            cardComponent.BodyName.SetText(moon.name);
            string aroundObjectName = celestialBodies.Planets.First(planet => planet.id == moon.aroundObjectId).name;
            string info;
            if (moon.vol == null)
            {
                info = $"Mass: {moon.mass} kg\n" +
               $"Mean radius: {moon.meanRadius} km\n" +
               $"Orbits around: {aroundObjectName}\n" +
               $"Semi-major Axis: {moon.semiMajorAxis} km\n" +
               $"Perigee: {moon.perigee} km\n" +
               $"Apogee: {moon.apogee} km\n" +
               $"Eccentricity: {moon.eccentricity}\n" +
               $"Inclination: {moon.inclination}°";
            }
            else
            {
                info = $"Mass: {moon.mass} kg\n" +
               $"Volume: {moon.vol} km^3\n" +
               $"Mean radius: {moon.meanRadius} km\n" +
               $"Orbits around: {aroundObjectName}\n" +
               $"Semi-major Axis: {moon.semiMajorAxis} km\n" +
               $"Perigee: {moon.perigee} km\n" +
               $"Apogee: {moon.apogee} km\n" +
               $"Eccentricity: {moon.eccentricity}\n" +
               $"Inclination: {moon.inclination}°";
            }
            cardComponent.Info.SetText(info);
        }
        length = (float)Math.Ceiling(length / 4);

        if (length < 2)
        {
            length = 800;
        }else{
            length = 615 * length + 18;
        }            
        container.sizeDelta = new Vector2(1800, length);
        cards.sizeDelta = new Vector2(1800, length);
        container.localPosition = new Vector2(0, -0.6f*length);
    }

    public void LoadAllCards()
    {
        LoadStarCards();
        LoadPlanetCards();
        LoadMoonCards();
        container.sizeDelta = new Vector2(1800, 27700);
        cards.sizeDelta = new Vector2(1800, 27700);
        container.localPosition = new Vector2(0, -0.6f*27700);
    }
}
