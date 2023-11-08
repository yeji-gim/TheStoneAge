using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalEntryPoint : MonoBehaviour
{
    [SerializeField]
    SceneTransitionManager.Location locationToSwitch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ãæµ¹");            /*if (SceneTransitionManager.Instance != null)
            {           
                
                SceneTransitionManager.Instance.SwitchLocation(locationToSwitch);
            }*/
            SceneManager.LoadScene(locationToSwitch.ToString());

        }

    }
}
