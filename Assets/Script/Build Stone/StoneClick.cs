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
    public GameObject[] handAxeItems;
    public GameObject[] stoneAxeItems;
    public GameObject[] spearItems;
    public GameObject[] arrowItems;

    public GameObject[] completeItems;
    public static int num;

    public GameObject Buttonobject;

    private void Awake()
    {
        if (num == 0)
        {
            for(int i = 0;i<handAxeItems.Length;i++)
            {
                Debug.Log(handAxeItems[i].name);
                handAxeItems[i].SetActive(true);
            }
            UIManager.Instance.maxCount -= 5;
            InvokeRepeating("InstantiateButton", 1f, 4f);
        }
        if (num == 1)
        {
            foreach (GameObject item in stoneAxeItems)
            {
                Debug.Log(item.name);
                item.SetActive(true);
            }
            UIManager.Instance.maxCount -= 2;
            InvokeRepeating("InstantiateButton", 1f, 3f);
        }
        if (num == 2)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 2;
            InvokeRepeating("InstantiateButton", 1f, 2f);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 5;
            InvokeRepeating("InstantiateButton", 1f, 1f);
        }
    }

    void Update()
    {
        if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
        {
            ActivateCompleteStone(num);
            CancelInvoke("InstantiateButton");
        }
        /*
        // 0: 주먹도끼
        if (num == 0)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 1: 돌 도끼
        if (num == 1)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 2: 창
        if (num == 2)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }

        // 3: 돌 화살
        if (num == 3)
        {
            if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount)
            {
                ActivateCompleteStone(num);
                // InvokeRepeating 멈추기
                CancelInvoke("InstantiateButton");
            }
        }
        */
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void ActivateCompleteStone(int num)
    {
        // 미완성 돌 이미지 비활성화
        if (num == 0)
        {
            foreach(GameObject item in handAxeItems)
                item.SetActive(false);
        }
        if (num == 1)
        {
            foreach(GameObject item in stoneAxeItems)
                item.SetActive(false);
        }
        if (num == 2)
        {
            foreach(GameObject item in spearItems)
                item.SetActive(false);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(false);
        }

        // 완성된 돌 이미지 활성화
        completeItems[num].SetActive(true);

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
