from dataclasses import dataclass

@dataclass
class Planet:
    id: int
    name: str
    mass:float
    vol:float
    aroundObjectId:int
    semiMajorAxis: float
    perihelion: float
    aphelion:float
    eccentricity:float
    inclination:float
    meanRadius:float
    
