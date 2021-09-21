using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAttack : MonoBehaviour
{
    Animator animator;
    BoxCollider2D boxCollider;

    void Awake()
    {
        animator = transform.parent.gameObject.GetComponent<Animator>();
        boxCollider = transform.parent.gameObject.GetComponent<BoxCollider2D>();
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "armBase")
        {
            //boxCollider.enabled = false;
            animator.SetBool("attack", true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "armBase")
        {
            animator.SetBool("attack", false);
            //boxCollider.enabled = true;
        }
    }
}
