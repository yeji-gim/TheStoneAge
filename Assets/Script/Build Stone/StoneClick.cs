using UnityEngine;
using UnityEngine.UI;

public class StoneClick : MonoBehaviour
{
    public GameObject incompleteStone; // 미완성 돌 이미지
    public GameObject completeStone;   // 완성된 돌 이미지

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f,3f);
    }

    void Update()
    {
        // 클릭 횟수가 10번에 도달하면 완성된 돌 이미지 활성화
        if (clickCount >= 10)
        {
            ActivateCompleteStone();
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600,600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone()
    {
        // 미완성 돌 이미지 비활성화
        incompleteStone.SetActive(false);

        // 완성된 돌 이미지 활성화
        completeStone.SetActive(true);
    }
    public void Success()
    {
        Buttonobject.SetActive(false);
        clickCount += 1;
    }

    public void Fail()
    {
        Buttonobject.SetActive(false);
    }
}
