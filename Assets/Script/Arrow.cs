using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public LayerMask AnimalLayer;
    
    public float AttackSize;
    public float speed;

    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, AttackSize);
    }

    void Update()
    {
        Collider[] _target = Physics.OverlapSphere(transform.position, AttackSize, AnimalLayer);

        for (int i = 0; i < _target.Length; i++)
        {
            if (_target[i].GetComponent<Animal>())
                _target[i].GetComponent<Animal>().hit();
            if (_target[i].GetComponent<Bear>())
                _target[i].GetComponent<Bear>().hit();
            if (_target[i].GetComponent<Plant>())
                _target[i].GetComponent<Plant>().hit();
            Destroy(gameObject);
        }
        if(rigid.velocity.magnitude != 0)
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
    }
    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
