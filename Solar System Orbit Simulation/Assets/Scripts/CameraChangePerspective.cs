using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Slider slider;
    private Vector3 startPosition = new(0, 0, -10);
    private Vector3 endPosition = new(0, -10, 0);
    private Quaternion startRotation = Quaternion.Euler(0, 0, 0);
    private Quaternion endRotation = Quaternion.Euler(-90, 0, 0);

    void Start()
    {
        slider.onValueChanged.AddListener(HandleSliderChanged);
        cam.transform.SetPositionAndRotation(startPosition, startRotation);
        slider.value = 0;
    }

    public void HandleSliderChanged(float value)
    {
        cam.transform.SetLocalPositionAndRotation(Vector3.Lerp(startPosition, endPosition, value), Quaternion.Lerp(startRotation, endRotation, value));
    }
}
