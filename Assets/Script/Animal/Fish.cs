using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    [SerializeField] private string animalName; // 동물의 이름
    public int hp;  // 동물의 체력

    // 필요한 컴포넌트
    private Animator anim;

    private float hpcul = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (hpcul > 0)
        {
            hpcul -= Time.deltaTime;
        }
    }

    public void Dead()
    {
        anim.SetTrigger("isDie");
        gameObject.layer = LayerMask.NameToLayer("Item");
    }

    public void hit()
    {
        if (hpcul <= 0)
        {
            hpcul = 1;
            hp -= 1;
            if (hp == 0)
            {
                Dead();
            }
        }
    }
}