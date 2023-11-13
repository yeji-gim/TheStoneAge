using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandMiniMap : MonoBehaviour
{
    private GameObject cam;
    public Camera mapCamera;
    public float big = 30.0f;
    public Slider mapZoom;

    void Start()
    {
        cam = GameObject.Find("MiniMapCamera");
        // mapCamera = cam.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        mapCamera.orthographicSize = big;
    }
    private void OnDisable()
    {
        mapCamera.orthographicSize = 40;
    }
}