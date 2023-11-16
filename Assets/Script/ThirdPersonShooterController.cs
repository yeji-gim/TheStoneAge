using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.UI;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private GameObject debugAim;
    [SerializeField] private Image AimUI;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;

    private float AimUIColor = 0;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            AimUIColor = 1;

            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCentorPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCentorPoint);

            debugAim.SetActive(true);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 10000f, aimColliderLayerMask))
            {
                debugAim.SetActive(true);
                debugAim.transform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }
            else
            {
                debugAim.SetActive(false);
            }

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            debugAim.SetActive(false);
            AimUIColor = 0;
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
        }
        AimUI.color = Color.Lerp(AimUI.color, new Color(1,1,1,AimUIColor), Time.deltaTime*2f);
        AimUI.transform.localScale = Vector3.Lerp(AimUI.transform.localScale, Vector3.one * (2-AimUIColor), Time.deltaTime * 2f);
    }
}
