using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StoneClick : MonoBehaviour
{
    public GameObject[] incompleteStone; // 미완성 돌 이미지
    public GameObject completeStone;   // 완성된 돌 이미지

    public Slider clickCountSlider;    // 클릭 횟수를 표시할 슬라이더
    public Text successText;           // 성공 텍스트
    public Text failText;              // 실패 텍스트
    public Text completeText;          // 완성 텍스트

    public int clickCount = 0;
    public int maxCount;

    public GameObject Buttonobject;

    private void Awake()
    {
        InvokeRepeating("InstantiateButton", 1f, 3f);
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
        Buttonobject.SetActive(false);
        clickCount += 1;

        // 슬라이더 업데이트
        UpdateSlider();

        // 성공 텍스트의 위치를 버튼의 위치로 조정
        Vector2 buttonPosition = Buttonobject.GetComponent<RectTransform>().anchoredPosition;
        successText.gameObject.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

        // 성공 텍스트 활성화
        successText.gameObject.SetActive(true);

        // 성공 텍스트 비활성화
        StartCoroutine(DelayedSuccessTextDeactivation());
    }

    // Fail 버튼과 onClick으로 연결
    public void Fail()
    {
        Buttonobject.SetActive(false);

        // 실패 텍스트의 위치를 버튼의 위치로 조정
        Vector2 buttonPosition = Buttonobject.GetComponent<RectTransform>().anchoredPosition;
        failText.gameObject.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

        // 실패 텍스트 활성화
        failText.gameObject.SetActive(true);

        // 실패 텍스트 비활성화
        StartCoroutine(DelayedFailTextDeactivation());
    }

    // 슬라이더 업데이트
    void UpdateSlider()
    {
        float normalizedValue = (float)clickCount / maxCount;
        clickCountSlider.value = normalizedValue;
    }

    // 시간 지연 후 성공 텍스트 비활성화
    IEnumerator DelayedSuccessTextDeactivation()
    {
        yield return new WaitForSeconds(1.0f); // 원하는 시간을 초 단위로 설정
        successText.gameObject.SetActive(false);
    }
    // 시간 지연 후 실패 텍스트 비활성화
    IEnumerator DelayedFailTextDeactivation()
    {
        yield return new WaitForSeconds(1.0f); // 원하는 시간을 초 단위로 설정
        failText.gameObject.SetActive(false);
    }
}
