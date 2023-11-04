using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void LoadSceneByEnum(craftManager.Scenename sceneName)
    {
        // 열거형 멤버를 사용하여 씬 이름을 선택하고 로드합니다.
        string sceneToLoad = sceneName.ToString();
        SceneManager.LoadScene(sceneToLoad);
    }
}
