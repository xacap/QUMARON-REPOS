using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionSystem;

public class Clock : MonoBehaviour
{
    [SerializeField] float timeSpeed;
    [SerializeField] GameObject Kuku;
    [SerializeField] GameObject Chicken;
    //[SerializeField] GameObject ChickenView;
    public Chicken chickenScr;
    Animator clockAnimator;
    //Animator chickenAnimator;
    public bool cuckooIsActive = false;

    private void Awake()
    {
        timeSpeed = 1f;
        chickenScr = Chicken.GetComponent<Chicken>();
        clockAnimator = this.GetComponent<Animator>();
        //chickenAnimator = Chicken.GetComponent<Animator>();
    }
    private void Update()
    {
        clockAnimator.speed = timeSpeed;
    }

    [System.Obsolete]
    public void KukushkaOn()
    {
        Kuku.active = true;
        cuckooIsActive = true;
        //CGameManager.Instance.GetNotificationManager().Invoke(EEventType.eAttentionCuckoo);
        chickenScr.ChickenWokeUpKuku(true);
        //chicken.kukuWokeUp = true;
    }

    [System.Obsolete]
    public void KukushkaOff()
    {
        //chicken.kukuWokeUp = false;

        if (Kuku.active)
        {
            Kuku.active = false;
            cuckooIsActive = false;
        }

        chickenScr.ChickenWokeUpKuku(false);
    }
}
