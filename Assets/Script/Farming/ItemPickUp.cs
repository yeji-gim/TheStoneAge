using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 5f;
    public LayerMask itemLayer;
    public Button pickupButton;
    public Text inventoryText;

    private GameObject nearbyItem;

    void Start()
    {
        pickupButton.gameObject.SetActive(false);
        inventoryText.text = "Inventory: 0";
        pickupButton.onClick.AddListener(PickupItem);
    }

    void Update()
    {
        CheckForNearbyItems();
    }

    void CheckForNearbyItems()
    {
        // SphereCast를 사용하여 주변에 아이템이 있는지 확인
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, pickupRadius, transform.forward, out hit, Mathf.Infinity, itemLayer))
        {
            nearbyItem = hit.collider.gameObject;

            // 아이템의 태그에 따라 버튼을 활성화
            if (nearbyItem.CompareTag("Branch") || nearbyItem.CompareTag("RockCluster") || nearbyItem.CompareTag("Fruit"))
            {
                ActivateButton();
            }
            else
            {
                DeactivateButton();
            }
        }
        else
        {
            // 주변에 아이템이 없으면 버튼 비활성화
            DeactivateButton();
            nearbyItem = null;
        }
    }


    void ActivateButton()
    {
        pickupButton.gameObject.SetActive(true);
    }

    void DeactivateButton()
    {
        pickupButton.gameObject.SetActive(false);
    }

    // PickUp 버튼과 onclick 연결
    void PickupItem()
    {
        // 아이템을 주웠을 때의 동작
        if (nearbyItem != null)
        {
            // 아이템의 태그와 수량을 메시지로 화면에 표시
            string itemTag = nearbyItem.tag;
            Debug.Log(itemTag + " +1"); // 실제로는 이 부분을 원하는 형태로 화면에 표시하는 방법으로 변경

            if (nearbyItem.CompareTag("Branch"))
            {
                inventoryText.text = "나뭇가지 +1";
                inventoryText.gameObject.SetActive(true);
                // 인벤토리에 나뭇가지 추가
            }

            else if (nearbyItem.CompareTag("RockCluster"))
            {
                inventoryText.text = "돌 조각 +1";
                inventoryText.gameObject.SetActive(true);
                // 인벤토리에 돌 조각 추가
            }

            else if (nearbyItem.CompareTag("Fruit"))
            {
                inventoryText.text = "과일 +1";
                inventoryText.gameObject.SetActive(true);
                // 인벤토리에 과일 추가
            }

            int inventoryCount = int.Parse(inventoryText.text.Substring("Inventory: ".Length));
            inventoryCount++;
            inventoryText.text = "Inventory: " + inventoryCount;

            // 아이템 삭제 및 버튼 비활성화
            Destroy(nearbyItem);
            DeactivateButton();
        }
    }
}
