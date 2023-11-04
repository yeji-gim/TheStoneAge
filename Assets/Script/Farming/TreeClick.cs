
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class TreeClick : MonoBehaviour
{
    public GameObject branchPrefab;

    private bool isGravityEnabled = false; // 중력 활성/비활성 상태를 나타내는 변수
    private GameObject currentBranch; // 현재 생성된 나뭇가지를 저장하는 변수

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tree"))
                {
                    // 클릭 시 중력 상태 토글 및 나뭇가지 생성
                    CreateBranch(hit.collider.gameObject);
                }
            }
        }
    }

    void CreateBranch(GameObject tree)
    {
        if (currentBranch != null)
        {
            // 이미 나뭇가지가 생성되어 있다면 삭제
            Destroy(currentBranch);
        }

        // 나뭇가지 프리팹을 생성
        currentBranch = Instantiate(branchPrefab, tree.transform.position - new Vector3(0, -9, 3), Quaternion.identity);
        currentBranch.SetActive(true);

        // 회전값을 설정하여 나뭇가지가 특정 각도로 회전되도록 함
        Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
        currentBranch.transform.rotation = rotation;

        //생성된 나뭇가지에 Rigidbody 및 중력 설정
        Rigidbody branchRigidbody = currentBranch.GetComponent<Rigidbody>();
        if (branchRigidbody != null)
        {
            branchRigidbody.useGravity = true;
        }
    }
}