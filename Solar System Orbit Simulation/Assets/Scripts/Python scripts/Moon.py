from dataclasses import dataclass

@dataclass
class Moon:
    id: int
    name: str
    mass:float 
    vol:float
    aroundObjectId:int
    semiMajorAxis: float
    perigee: float
    apogee:float
    eccentricity:float
    inclination:float
    meanRadius:float