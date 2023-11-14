using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPlayer : MonoBehaviour
{
    public Animator animator;
    public Animator[] peopleanimator;

    private void Start()
    {

    }

    public void event1()
    {
        animator.SetBool("isDance", true);
    }

    public void event2()
    {
        animator.SetBool("isWalk", true);
    }

    public void event3()
    {
        for (int i = 0; i < peopleanimator.Length; i++)
        {
            peopleanimator[i].SetBool("isDance", true);
        }
    }
}
