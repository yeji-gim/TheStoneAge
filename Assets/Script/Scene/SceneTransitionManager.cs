using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    Transform playerPoint;

    public enum SceneState
    {
        Map,Cave,
    }
    public SceneState sceneState;

    public enum Location { Map, Cave }
    public Location currentLocation;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerPoint = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void SwitchLocation(Location locationToSwitch)
    {
        if (currentLocation == locationToSwitch)
        {
            Debug.LogWarning("Already in the target location.");
            return;
        }

        string sceneName = locationToSwitch.ToString();
        SceneManager.LoadScene(sceneName);
    }
}
