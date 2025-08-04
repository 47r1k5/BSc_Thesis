using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSimulationSpeed : MonoBehaviour
{
    public Sun sun;
    [SerializeField] private Slider slider;
    [SerializeField] private Camera cam;
    public CameraFocus focus;
    private float dt;
    void Start()
    {
        focus = cam.GetComponent<CameraFocus>();
        GameObject sunObject = GameObject.FindGameObjectWithTag("Sun");
        sun = sunObject.GetComponent<Sun>();
        sun.dt = 5000;
        dt = sun.dt;
        slider.value = dt;
        slider.onValueChanged.AddListener(HandleSliderChanged);
    }

    public void HandleSliderChanged(float value)
    {
        if (sun != null) sun.dt = value;
        else
        {
            Planet focusCelestialBody = focus.target.gameObject.GetComponent<Planet>();
            focusCelestialBody.dt = value;
        }
    }
}
