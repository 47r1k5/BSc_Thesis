using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraFocus : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 focusOffset = new(0, 0, -10);
    public Vector3 defaultPosition = new(0, 0, -10);
    public Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
    public float zoomSize = 5f;
    public float defaultZoomSize = 10f;
    public Transform target;
    [SerializeField] private Camera cam;
    [SerializeField] private CelestialBodiesLoader loader;
    [SerializeField] private Slider slider;
    private float zoomVelocity = 0f;
    private readonly float smoothTime = 0.25f;
    private readonly float originalSize = 10f;
    private bool isFocused = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.CompareTag("Planet"))
                {
                    if (target != hit.transform && !isFocused)
                    {
                        target = hit.transform;
                        DestroyCelestialBodiesExceptTarget(target);
                        FocusOnCelestialBody(target);
                        isFocused = true;
                    }
                    else if (isFocused)
                    {
                        ResetFocusAndReloadCelestialBodies();
                    }
                }
            }
        }
    }

    void DestroyCelestialBodiesExceptTarget(Transform targetObject)
    {
        foreach (var planet in GameObject.FindGameObjectsWithTag("Planet"))
        {
            if (planet.transform != targetObject)
            {
                Destroy(planet);
            }
        }
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");
        if (sun != null)
        {
            Destroy(sun);
        }
        loader.LoadMoonsOfPlanet(targetObject.gameObject);
        slider.maxValue = 100;
        slider.value = 50;
        if (targetObject.gameObject.GetComponent<Planet>().name == "Mars")
        {
            slider.value = 0.25f;
        }

    }

    void FocusOnCelestialBody(Transform target)
    {
        cam.transform.SetParent(target);
        cam.transform.localPosition=defaultPosition;
    }

    void ResetFocusAndReloadCelestialBodies()
    {
        Destroy(target.gameObject);
        loader.LoadSunAndPlanets();
        cam.transform.SetPositionAndRotation(Vector3.Lerp(cam.transform.position, defaultPosition, smoothSpeed), Quaternion.Lerp(cam.transform.rotation, defaultRotation, smoothSpeed));
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, defaultZoomSize, ref zoomVelocity, smoothTime);

        cam.transform.SetParent(null);
        cam.enabled=true;
        cam.GetComponent<Zoom>().enabled=true;
        cam.GetComponent<CameraFocus>().enabled=true;
        cam.orthographicSize = originalSize;
        cam.transform.position = defaultPosition;
        slider.maxValue = 5000;
        slider.value = 5000;
        slider.GetComponent<ChangeSimulationSpeed>().sun = loader.sun;
        target = null;
        isFocused = false;
    }

    void FocusOnPlanetFromCards(string name){
        if(name!="Sun"){
            foreach (var planet in GameObject.FindGameObjectsWithTag("Planet"))
            {
                if(planet.name==name){
                    target=planet.transform;
                    DestroyCelestialBodiesExceptTarget(target);
                    FocusOnCelestialBody(target);
                    isFocused = true;
                    break;
                }
            }
        }
    }
}
