using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAnimal : MonoBehaviour
{
    public float speed;
    public bool isRun;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        if (isRun)
        {
            GetComponent<Animator>().SetBool("isRun", true);
        }
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * speed;
    }
}
