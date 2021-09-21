using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRoomCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("obstacle") || collision.CompareTag("web") || collision.CompareTag("lamp"))
        {
            Player.Instance.ColliderList.Add(collision.gameObject);
            //Debug.Log(" CRoomCondition / obstacle name : " + collision.gameObject);
        }

        if (collision.CompareTag("clock"))
         {
            Chicken.Instance.ClockList.Add(collision.gameObject);
            //Debug.Log(" CRoomCondition / Clock name : " + collision.gameObject);
        }
    }
}
