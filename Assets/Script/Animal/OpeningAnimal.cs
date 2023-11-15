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
        rigid = GetComponent<Rigidbody>();
        if (isRun)
        {
            Invoke("Run", Random.Range(0f, 1.0f));
        }
    }
    public void Run()
    {
        GetComponent<Animator>().SetBool("isRun", true);
    }

    // Update is called once per frame
    void Update()
    {
        starttime -= Time.deltaTime;
        if(starttime<0)
            rigid.velocity = new Vector3(transform.forward.x * speed,rigid.velocity.y, transform.forward.z * speed);
    }
}
