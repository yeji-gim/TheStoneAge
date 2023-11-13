using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandMiniMap : MonoBehaviour
{
    public GameObject Extended;
    public GameObject Minimized;
    public Camera mapCamera;
    public float big = 30.0f;
    private float origin;
    public Slider mapZoom;

    void Start()
    {
        origin = mapCamera.orthographicSize;
    }

    void Update()
    {
        if (Extended.activeSelf)
        {
            mapCamera.orthographicSize = big;
        }

        if (Minimized.activeSelf)
        {
            mapCamera.orthographicSize = origin;
        }
    }
}