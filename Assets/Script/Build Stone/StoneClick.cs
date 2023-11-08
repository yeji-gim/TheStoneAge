using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StoneClick : MonoBehaviour
{
    public GameObject[] incompleteStone; // 미완성 돌 이미지
    public GameObject completeStone;   // 완성된 돌 이미지

    public Slider clickCountSlider;    // 클릭 횟수를 표시할 슬라이더
    public TMP_Text completeText;          // 완성 텍스트

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 4f);
    }

    void Update()
    {
        // 클릭 횟수가 10번에 도달하면 완성된 돌 이미지 활성화
        if (clickCount >= maxCount)
        {
            ActivateCompleteStone();
            // InvokeRepeating 멈추기
            CancelInvoke("InstantiateButton");
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone()
    {
        // 미완성 돌 이미지 비활성화
        foreach (GameObject item in incompleteStone)
        {
            item.SetActive(false);
        }

        // 완성된 돌 이미지 활성화
        completeStone.SetActive(true);

        // 완성 텍스트
        completeText.gameObject.SetActive(true);
    }

    // Success 버튼과 onClick으로 연결
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        clickCount += 1;

        // 슬라이더 업데이트
        UpdateSlider();

    }

    // Fail 버튼과 onClick으로 연결
    public void Fail()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isFail");
    }

    // 슬라이더 업데이트
    void UpdateSlider()
    {
        float normalizedValue = (float)clickCount / maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
