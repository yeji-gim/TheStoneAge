using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetObjectInfo : MonoBehaviour
{
    public Camera getCamera;
    public GameObject Panel;
    private RaycastHit hit;
    // Start is called before the first frame update
    /*
    public void openPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = getCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                string objectTag = hit.collider.gameObject.tag;
                bool isActive = Panel.activeSelf;

                switch (objectTag)
                {
                    case "Default":
                        Panel.SetActive(!isActive);
                        break;

                }

            }
        }
    }
}
