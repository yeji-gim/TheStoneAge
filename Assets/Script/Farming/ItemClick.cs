using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    public Canvas uiCanvasPrefab; // UI 캔버스 프리팹
    public Canvas uiCanvas;

    void Start()
    {
        // 씬에 프리팹이 없다면 새로운 인스턴스를 생성하여 할당
        if (uiCanvasPrefab != null && uiCanvas == null)
        {
            uiCanvas = Instantiate(uiCanvasPrefab);
            uiCanvas.enabled = false;
        }
    }

    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 레이캐스트를 이용하여 아이템을 클릭했는지 확인
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Item"))
            {
                // 아이템을 클릭하면 알림창 UI 캔버스 활성화
                uiCanvas.enabled = true;
            }
        }
    }
}
