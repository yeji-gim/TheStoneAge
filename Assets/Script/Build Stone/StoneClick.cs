using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class StoneClick : MonoBehaviour
{
    public Slider clickCountSlider;    // 클릭 횟수를 표시할 슬라이더
    public TMP_Text completeText;      // 완성 텍스트
    public GameObject[] handAxeItems;
    public GameObject[] stoneAxeItems;
    public GameObject[] spearItems;
    public GameObject[] arrowItems;

    public EquipmentData[] equipment;
    public GameObject[] completeItems;
    public static int num;

    public GameObject Buttonobject;

    public ParticleSystem StoneStun;

    public AudioClip clickSound;
    public AudioClip completeSound;
    private AudioSource clickAudio;
    private AudioSource completeAudio;
    private AudioSource backgroundAudio;

    private bool hasActivatedCompleteStone = false;

    private void Start()
    {
        UIManager.Instance.clickCount = 0;
        if (num == 0)
        {
            foreach (GameObject item in handAxeItems)
                item.SetActive(true);
            UIManager.Instance.maxCount -= 5;
            InvokeRepeating("InstantiateButton", 1f, 4f);
        }
        if (num == 1)
        {
            foreach (GameObject item in stoneAxeItems)
                item.SetActive(true);
            UIManager.Instance.maxCount *= 2;
            InvokeRepeating("InstantiateButton", 0.8f, 3f);
        }
        if (num == 2)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 2;
            InvokeRepeating("InstantiateButton", 0.7f, 2f);
        }
        if (num == 3)
        {
            foreach (GameObject item in arrowItems)
                item.SetActive(true);
            UIManager.Instance.maxCount += 5;
            InvokeRepeating("InstantiateButton", 0.5f, 1f);
        }
        // AudioSource 컴포넌트 추가
        clickAudio = gameObject.AddComponent<AudioSource>();
        completeAudio = gameObject.AddComponent<AudioSource>();
        backgroundAudio = Camera.main.gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (UIManager.Instance.clickCount >= UIManager.Instance.maxCount && !hasActivatedCompleteStone)
        {
            ActivateCompleteStone(num);
            CancelInvoke("InstantiateButton");
            hasActivatedCompleteStone = true;
        }
    }

    void InstantiateButton()
    {
        Buttonobject.SetActive(false);
        Buttonobject.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-600, 600), Random.Range(-200, 200));
        Buttonobject.SetActive(true);
    }

    void GetItem(EquipmentData item)
    {
        ItemSlotData[] items = InventoryManager.Instance.GetInventorySlots(InventorySlot.InventoryType.Tool);


        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemData == null)
            {
                items[i].itemData = item;
                items[i].AddQuantity();
                item = null;
                break;
            }
        }
        UIManager.Instance.RenderInventory();
    }

    EquipmentData GetName(string name)
    {
        if (name.Contains("HandAxe"))
            return equipment[0];
        if (name.Contains("StoneAxe"))
            return equipment[1];
        if (name.Contains("Spear"))
            return equipment[2];
        if (name.Contains("Arrow"))
            return equipment[0];
        return equipment[0];
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

        // 완성 텍스트
        completeText.gameObject.SetActive(true);

        CameraManager.Instance.MoveCamera(new Vector3(120, 6, 5));

        StoneStun.Play();
        // 효과음 재생
        if (completeSound != null && completeAudio != null)
        {
            completeAudio.PlayOneShot(completeSound);
        }

        // 완성된 돌 이미지 활성화
        completeItems[num].SetActive(true);
        GetItem(GetName(completeItems[num].name));
        StartCoroutine(goCave());
    }

    IEnumerator goCave()
    {
        yield return new WaitForSeconds(3.0f);
        CameraManager.Instance.MoveCamera(new Vector3(120, 3, 5));
        SceneManager.LoadScene("Cave");
    }

    // Success 버튼과 onClick으로 연결
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

        // 효과음 재생
        if (clickSound != null && clickAudio != null)
        {
            clickAudio.PlayOneShot(clickSound);
        }

        // 슬라이더 업데이트
        UpdateSlider();

        // num에 따라 애니메이션 속도 조절
        float animationSpeed = 1.0f; // 기본 속도

        switch (num)
        {
            case 0:
                animationSpeed = 1.0f; // num == 0에 대한 속도 설정
                break;
            case 1:
                animationSpeed = 1.5f; // num == 1에 대한 속도 설정
                break;
            case 2:
                animationSpeed = 2.0f; // num == 2에 대한 속도 설정
                break;
            case 3:
                animationSpeed = 2.5f; // num == 3에 대한 속도 설정
                break;
            default:
                break;
        }

        Buttonobject.GetComponent<Animator>().speed = animationSpeed;
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
