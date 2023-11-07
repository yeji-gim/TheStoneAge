
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public GameObject childPrefab;//자식
    public int childCount;//자식 생성할 개수

    public void hit()
    {
        for (int i = 0; i < childCount; i++)
        {
            // 자식 프리팹을 생성
            GameObject currentBranch = Instantiate(childPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);

            // 회전값을 설정하여 나뭇가지가 특정 각도로 회전되도록 함
            Quaternion rotation = Quaternion.Euler(Random.Range(0f, 360f), 0f, Random.Range(0f, 360f));
            currentBranch.transform.rotation = rotation;

            //생성된 나뭇가지에 Rigidbody 및 중력 설정
            Rigidbody branchRigidbody = currentBranch.GetComponent<Rigidbody>();

            branchRigidbody.AddForce(currentBranch.transform.forward*10);

        }

        Destroy(gameObject);
    }
}