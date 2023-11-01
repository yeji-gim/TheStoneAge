using UnityEngine;
using UnityEngine.UI;

public class StoneClick : MonoBehaviour
{
    public GameObject incompleteStone; // 미완성 돌 이미지
    public GameObject completeStone;   // 완성된 돌 이미지

    private int clickCount = 0;

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트를 이용하여 돌을 클릭했는지 확인
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Stone"))
            {
                // 돌을 클릭하면 clickCount 증가
                clickCount++;

                // 클릭 횟수가 10번에 도달하면 완성된 돌 이미지 활성화
                if (clickCount >= 10)
                {
                    ActivateCompleteStone();
                }
            }
        }
    }

    void ActivateCompleteStone()
    {
        // 미완성 돌 이미지 비활성화
        incompleteStone.SetActive(false);

        // 완성된 돌 이미지 활성화
        completeStone.SetActive(true);
    }
}
