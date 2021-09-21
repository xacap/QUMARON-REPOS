using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionSystem;

public class Chicken : MonoBehaviour
{
    [SerializeField] public Vector2 longSight;
    [SerializeField] GameObject clock;
    [SerializeField] GameObject attackCollider;
    Rigidbody2D _rb;
    BoxCollider2D boxCollider;
    Animator animator;
    bool danger = false;
    bool kukuWokeUp = false;
    private Vector3 thisPos;
    private Vector3 clockPos;

    public static Chicken Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Chicken>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Chicken");
                    instance = instanceContainer.AddComponent<Chicken>();
                }
            }
            return instance;
        }
    }
    private static Chicken instance;

    public List<GameObject> ClockList = new List<GameObject>();
    Vector3 cuckooPos;

    [System.Obsolete]
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        clockPos = clock.transform.position;
        //CGameManager.Instance.GetNotificationManager().Register(EEventType.eAttentionCuckoo, ChickenWokeUpKuku);
    }
    private void Update()
    {
        if (danger)
        {
            animator.SetBool("flip", true);
        }

        boxCollider.size = longSight;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "armBase")
        {
            danger = true;

            if (this.transform.position.x >= collision.gameObject.transform.position.x)
            {
                _rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "armBase")
        {
            animator.SetBool("flip", false);
            danger = false;
        }
    }

    [System.Obsolete]
    public void ChickenWokeUpKuku(bool isBool)
    {
        //cuckooPos = ClockList[i].transform.position;

        if (isBool)
        {
            animator.SetBool("flip", true);
            boxCollider.enabled = false;
            attackCollider.active = false;

            if (this.transform.position.x >= clockPos.x)
            {
                _rb.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _rb.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else
        {
            animator.SetBool("flip", false);
            boxCollider.enabled = true;
            attackCollider.active = true;
            //_rb.transform.rotation = Quaternion.Euler(0, 0, 0); // Cделать до завершения анимации.
        }
    }
}
