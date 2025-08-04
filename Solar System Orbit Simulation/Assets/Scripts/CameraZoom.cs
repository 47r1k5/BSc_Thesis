using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{        
    private float zoom;
    private readonly float zoomMultiplier = 8f;
    private readonly float minZoom = 2f;
    private readonly float maxZoom = 120f;
    private float zoomVelocity = 0f;
    private readonly float smoothTime = 0.25f;
    [SerializeField] private Camera cam;
    void Start()
    {
        zoom = cam.orthographicSize;
    }

    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref zoomVelocity, smoothTime);
    }

}
