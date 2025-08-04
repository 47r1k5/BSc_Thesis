import json
import Planet
import Sun
import Moon
import dataclasses

def main():
    response_file=f"C:\\Users\\patri\\Desktop\\University\\SolarSystemThesis\\Solar System Orbit Simulation\\Assets\\Scripts\\Python scripts\\response_data.json"
    
    """
    response = requests.get("https://api.le-systeme-solaire.net/rest/bodies")

    with open(response_file, "w") as file:
        json.dump(response, file)
        file.close()
    """    

    with open(response_file, 'r') as file:
        data=json.load(file)
        data=data['bodies']
        file.close()

    starsList=[]
    for body in data:
        if body["bodyType"]=="Star":
            sunId=1
            strId=body["id"]
            name=body["englishName"]
            mass=float(f"{body["mass"]["massValue"]}E{body["mass"]["massExponent"]}")
            vol=float(f"{body["vol"]["volValue"]}E{body["vol"]["volExponent"]}")
            meanRadius=body["meanRadius"]
            sun = Sun.Sun(id=sunId, name=name, mass=mass, vol=vol, meanRadius=meanRadius)
            starsList.append(sun)
            break

    starsDictList=[]
    for p in planetsList:
        starsDictList.append(dataclasses.asdict(sun))

    bodyId=sun.id+1
    planetsList=[]
    planetIdsDict={}
    for body in data:
        if body["bodyType"]=="Planet":
            strId=body["id"]
            name=body["englishName"]
            semiMajorAxis=body["semimajorAxis"]
            perihelion=body["perihelion"]
            aphelion=body["aphelion"]
            mass=float(f"{body["mass"]["massValue"]}E{body["mass"]["massExponent"]}")
            vol=float(f"{body["vol"]["volValue"]}E{body["vol"]["volExponent"]}")
            eccentricity=body["eccentricity"]
            inclination=body["inclination"]
            meanRadius=body["meanRadius"]
            planet = Planet.Planet(id=bodyId, name=name, mass=mass, vol=vol,aroundObjectId=sun.id,
                                   semiMajorAxis=semiMajorAxis,perihelion=perihelion,aphelion=aphelion, 
                                   eccentricity=eccentricity, inclination=inclination, meanRadius=meanRadius)
            idDict={strId:bodyId}
            planetsList.append(planet)
            planetIdsDict.update(idDict)
            bodyId+=1


    planetsDictList=[]
    for p in planetsList:
        planetsDictList.append(dataclasses.asdict(p))

    moonsList=[]
    for body in data:
        if body["bodyType"]=="Moon":
            if body["mass"]!=None:
                strId=body["id"]
                name=body["englishName"]
                semiMajorAxis=body["semimajorAxis"]
                perihelion=body["perihelion"]
                aphelion=body["aphelion"]
                aroundBody=planetIdsDict.get(body["aroundPlanet"]["planet"])
                eccentricity=body["eccentricity"]
                inclination=body["inclination"]
                meanRadius=body["meanRadius"]
                mass=float(f"{body["mass"]["massValue"]}E{body["mass"]["massExponent"]}")
                if (perihelion<=0 or aphelion<=0):
                    perihelion=semiMajorAxis*(1-eccentricity)
                    aphelion=semiMajorAxis*(1+eccentricity)
                    if body["vol"]!=None:
                        vol=float(f"{body["vol"]["volValue"]}E{body["vol"]["volExponent"]}")
                    else:
                        vol=None
                if aroundBody!=None:
                    moon = Moon.Moon(id=bodyId, name=name, mass=mass, vol=vol,aroundObjectId=aroundBody,
                                     semiMajorAxis=semiMajorAxis,perigee=perihelion,apogee=aphelion, eccentricity=eccentricity, 
                                     inclination=inclination, meanRadius=meanRadius)
                    bodyId+=1
                    moonsList.append(moon)

    planetsDictList=[]
    for p in planetsList:
        planetsDictList.append(dataclasses.asdict(p))

    moonsDictList=[]
    for m in moonsList:
        moonsDictList.append(dataclasses.asdict(m))

    bodies={"stars":starsDictList,"planets":planetsDictList,"moons":moonsDictList}

    bodies_file=f"C:\\Users\\patri\\Desktop\\University\\SolarSystemThesis\\Solar System Orbit Simulation\\Assets\\celestial_bodies.json"

    with open(bodies_file, "w") as file:
        json.dump(bodies, file)
        file.close()

if __name__ == "__main__":
    main()