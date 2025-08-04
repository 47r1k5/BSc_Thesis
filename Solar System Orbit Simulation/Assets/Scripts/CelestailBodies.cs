using System.Collections.Generic;

/// <summary>
/// Represents a collection of celestial bodies including stars, planets, and moons.
/// </summary>
[System.Serializable]
public class CelestialBodies
{
    /// <summary>
    /// A list of star entities within the celestial collection.
    /// </summary>
    public List<StarEntity> Stars { get; set; }

    /// <summary>
    /// A list of planet entities within the celestial collection.
    /// </summary>
    public List<PlanetEntity> Planets { get; set; }

    /// <summary>
    /// A list of moon entities within the celestial collection.
    /// </summary>
    public List<MoonEntity> Moons { get; set; }

}

/// <summary>
/// Represents a star with its basic physical properties.
/// </summary>
[System.Serializable]
public class StarEntity
{
    /// <summary>
    /// Unique identifier for the star.
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Name of the star.
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// Mass of the star in kg.
    /// </summary>
    public float mass { get; set; }

    /// <summary>
    /// Volume of the star in cubic kilometers.
    /// </summary>
    public float vol { get; set; }

    /// <summary>
    /// Mean radius of the object in kilometers
    /// </summary>
    public float meanRadius { get; set; }
}

/// <summary>
/// Represents a planet with its basic physical and orbital properties.
/// </summary>
[System.Serializable]
public class PlanetEntity
{
    /// <summary>
    /// Unique identifier for the planet.
    /// </summary>
    public int id { get; set; }
    /// <summary>
    /// Name of the planet.
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// Mass of the planet in kg.
    /// </summary>
    public float mass { get; set; }

    /// <summary>
    /// Volume of the planet in cubic kilometers.
    /// </summary>
    public float vol { get; set; }

    /// <summary>
    /// Mean radius of the object in kilometers
    /// </summary>
    public float meanRadius { get; set; }

    /// <summary>
    /// ID of the celestial body around which the planet orbits.
    /// </summary>
    public int aroundObjectId { get; set; }

    /// <summary>
    /// Length of the semi-major axis of the planet's orbit in kilometers.
    /// </summary>
    public float semiMajorAxis { get; set; }

    /// <summary>
    /// Closest distance to the object it orbits in kilometers.
    /// </summary>
    public float perihelion { get; set; }

    /// <summary>
    /// Farthest distance from the object it orbits in kilometers.
    /// </summary>
    public float aphelion { get; set; }

    /// <summary>
    /// Orbital eccentricity, indicating how much the orbit deviates from a perfect circle.
    /// </summary>
    public float eccentricity { get; set; }

    /// <summary>
    /// The deviation from the horizon of the around object in degrees.
    /// </summary>
    public float inclination { get; set; }
}

/// <summary>
/// Represents a moon with its basic and orbital physical properties.
/// </summary>
[System.Serializable]
public class MoonEntity
{
    /// <summary>
    /// Unique identifier for the moon.
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Name of the moon.
    /// </summary>
    public string name { get; set; }

    /// <summary>
    /// Mass of the moon in kg.
    /// </summary>
    public float mass { get; set; }

    /// <summary>
    /// Volume of the moon in cubic kilometers.
    /// </summary>
    public float? vol { get; set; }

    /// <summary>
    /// ID of the celestial body around which the moon orbits.
    /// </summary>
    public int aroundObjectId { get; set; }

    /// <summary>
    /// Length of the semi-major axis of the moon's orbit in kilometers.
    /// </summary>
    public float semiMajorAxis { get; set; }

    /// <summary>
    /// Closest distance to the planet it orbits in kilometers.
    /// </summary>
    public float perigee { get; set; }

    /// <summary>
    /// Farthest distance from the planet it orbits in kilometers.
    /// </summary>
    public float apogee { get; set; }

    /// <summary>
    /// Orbital eccentricity, indicating how much the orbit deviates from a perfect circle.
    /// </summary>
    public float eccentricity { get; set; }

    /// <summary>
    /// The deviation from the horizon of the around object in degrees.
    /// </summary>
    public float inclination { get; set; }

    /// <summary>
    /// Mean radius of the object in kilometers
    /// </summary>
    public float meanRadius { get; set; }
}