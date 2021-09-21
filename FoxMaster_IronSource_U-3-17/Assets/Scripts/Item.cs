using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody2D _rb;
    bool isTaken = false;
    GameObject arm;
    Arm armScr;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        arm = GameObject.FindGameObjectWithTag("armBase");
        armScr = arm.GetComponent<Arm>();
;    }
    private void Update()
    {
        if (isTaken)
        {
            _rb.transform.position = arm.transform.position;
            
            if(armScr != null) armScr.TakeEgg();
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "arm")
        {
            isTaken = true;
        }
    }
}
