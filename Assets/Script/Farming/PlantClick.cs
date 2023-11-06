
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class PlantClick : MonoBehaviour
{
    public GameObject fruitPrefab;

    private bool isGravityEnabled = false; // 중력 활성/비활성 상태를 나타내는 변수
    private GameObject currentFruit; // 현재 생성된 열매를 저장하는 변수

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Plant"))
                {
                    // 클릭 시 중력 상태 토글 및 열매 생성
                    CreateFruit(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateFruit(GameObject plant)
    {
        if (currentFruit != null)
        {
            // 이미 돌 조각이 생성되어 있다면 삭제
            Destroy(currentFruit);
        }

        // 열매 프리팹을 생성
        currentFruit = Instantiate(fruitPrefab, plant.transform.position - new Vector3(0, -2, 1), Quaternion.identity);
        currentFruit.SetActive(true);

        // 회전값을 설정하여 돌 조각이 특정 각도로 회전되도록 함
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentFruit.transform.rotation = rotation;

        //생성된 돌 조각에 Rigidbody 및 중력 설정
        Rigidbody branchRigidbody = currentFruit.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}