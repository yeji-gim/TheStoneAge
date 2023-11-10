using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;

public class StoneClick : MonoBehaviour
{
    public Slider clickCountSlider;    // 클릭 횟수를 표시할 슬라이더
    public TMP_Text completeText;      // 완성 텍스트
    
    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 4f);
    }

    void Update()
    {
        // 0: 주먹도끼
        if (UIManager.Instance.itemNo == 0)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 5)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
        // 1: 돌 도끼
        if (UIManager.Instance.itemNo == 1)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount - 2)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
        // 2: 창
        if (UIManager.Instance.itemNo == 2)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount +2)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
        // 3: 돌 화살
        if (UIManager.Instance.itemNo == 3)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount + 5)
            {
                ActivateCompleteStone(UIManager.Instance.itemNo);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone(int i)
    {
        // 미완성 돌 이미지 비활성화
        foreach (GameObject item in UIManager.Instance.incompleteStone[UIManager.Instance.itemNo])
        {
            item.SetActive(false);
        }

        // 완성된 돌 이미지 활성화
        UIManager.Instance.completeStone[UIManager.Instance.itemNo].SetActive(true);

        // 완성 텍스트
        completeText.gameObject.SetActive(true);
    }

    // Success 버튼과 onClick으로 연결
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

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
        float normalizedValue = (float)UIManager.Instance.clickCount / UIManager.Instance.maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
