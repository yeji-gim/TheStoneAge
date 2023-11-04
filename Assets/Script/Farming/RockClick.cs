
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class RockClick : MonoBehaviour
{
    public GameObject rockPrefab;

    private bool isGravityEnabled = false; // 중력 활성/비활성 상태를 나타내는 변수
    private GameObject currentRock; // 현재 생성된 돌 조각을 저장하는 변수

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Stone"))
                {
                    // 클릭 시 중력 상태 토글 및 돌 조각 생성
                    CreateRock(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateRock(GameObject tree)
    {
        if (currentRock != null)
        {
            // 이미 돌 조각이 생성되어 있다면 삭제
            Destroy(currentRock);
        }

        // 돌 조각 프리팹을 생성
        currentRock = Instantiate(rockPrefab, tree.transform.position - new Vector3(2, -3, 0), Quaternion.identity);
        currentRock.SetActive(true);

        // 회전값을 설정하여 돌 조각이 특정 각도로 회전되도록 함
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentRock.transform.rotation = rotation;

        //생성된 돌 조각에 Rigidbody 및 중력 설정
        Rigidbody branchRigidbody = currentRock.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}