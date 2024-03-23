using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public class StoneClick : MonoBehaviour
{
    public Slider clickCountSlider;    // Ŭ�� Ƚ���� ǥ���� �����̴�
    public TMP_Text completeText;      // �ϼ� �ؽ�Ʈ
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
        // AudioSource ������Ʈ �߰�
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
        ItemSlotData[] items = InventoryManager.Instance.getInventorySlots(InventorySlot.InventoryType.Tool);


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
        UIManager.Instance.renderInventory();
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
            return equipment[3];
        return equipment[0];
    }
    void ActivateCompleteStone(int num)
    {
        // �̿ϼ� �� �̹��� ��Ȱ��ȭ
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

        // �ϼ� �ؽ�Ʈ
        completeText.gameObject.SetActive(true);

        CameraManager.Instance.MoveCamera(new Vector3(120, 6, 5));

        StoneStun.Play();
        // ȿ���� ���
        if (completeSound != null && completeAudio != null)
        {
            completeAudio.PlayOneShot(completeSound);
        }

        // �ϼ��� �� �̹��� Ȱ��ȭ
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

    // Success ��ư�� onClick���� ����
    public void Success()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isSuccess");
        UIManager.Instance.clickCount += 1;

        // ȿ���� ���
        if (clickSound != null && clickAudio != null)
        {
            clickAudio.PlayOneShot(clickSound);
        }

        // �����̴� ������Ʈ
        UpdateSlider();

        // num�� ���� �ִϸ��̼� �ӵ� ����
        float animationSpeed = 1.0f; // �⺻ �ӵ�

        switch (num)
        {
            case 0:
                animationSpeed = 1.0f; // num == 0�� ���� �ӵ� ����
                break;
            case 1:
                animationSpeed = 1.5f; // num == 1�� ���� �ӵ� ����
                break;
            case 2:
                animationSpeed = 2.0f; // num == 2�� ���� �ӵ� ����
                break;
            case 3:
                animationSpeed = 2.5f; // num == 3�� ���� �ӵ� ����
                break;
            default:
                break;
        }

        Buttonobject.GetComponent<Animator>().speed = animationSpeed;
    }

    // Fail ��ư�� onClick���� ����
    public void Fail()
    {
        Buttonobject.GetComponent<Animator>().SetTrigger("isFail");
    }

    // �����̴� ������Ʈ
    void UpdateSlider()
    {
        float normalizedValue = (float)UIManager.Instance.clickCount / UIManager.Instance.maxCount;
        clickCountSlider.value = normalizedValue;
    }
}
