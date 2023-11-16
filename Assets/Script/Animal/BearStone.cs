using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class BearStone : MonoBehaviour
{
    [SerializeField] float rotateSpeed;//1초당 회전할 속도
    public float t;
    public bool ishit = false;

    void Update()
    {
        t += Time.deltaTime;
        transform.rotation = Quaternion.Euler(t * rotateSpeed, t * rotateSpeed, t * rotateSpeed);
        if (t * rotateSpeed >= 360) t = 0; //t값이 너무 커지지 않게 조절

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Animal")
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<ThirdPersonController>().hit();
                GetComponent<AudioSource>().Play();
                if (!ishit)
                    Destroy(gameObject, 1f);
                ishit = true;
            }
            else
            {
                if (!ishit)
                {
                    GetComponent<AudioSource>().Play();
                    ishit = true;
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
}
