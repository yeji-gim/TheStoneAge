using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MoveCamera(Vector3 targetPosition)
    {
        Camera.main.transform.position = targetPosition;
    }
}
