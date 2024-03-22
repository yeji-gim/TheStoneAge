using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadSceneByEnum(craftManager.Scenename sceneName)
    {
        string sceneToLoad = sceneName.ToString();
        SceneManager.LoadScene(sceneToLoad);
    }
}
