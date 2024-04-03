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
            //StartCoroutine(LoadSceneAsync(locationToSwitch.ToString()));
            SceneManager.LoadScene(locationToSwitch.ToString());

        }

    }
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // �ε� ���� ���� Ȯ��
        while (!asyncLoad.isDone)
        {
            // ���� ���¸� �����ִ� ��� �߰� ����
            yield return null;
        }

    }
   
}
