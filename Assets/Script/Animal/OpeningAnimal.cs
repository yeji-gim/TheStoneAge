using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningAnimal : MonoBehaviour
{
    public float speed;
    public bool isRun;
    Rigidbody rigid;

    public float starttime = 0f;

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
        starttime -= Time.deltaTime;
        if(starttime<0)
            rigid.velocity = new Vector3(transform.forward.x * speed,rigid.velocity.y, transform.forward.z * speed);
    }
}
