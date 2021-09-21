using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Arm : MonoBehaviour
{
    [SerializeField] GameObject PlayerObj;
    Player playerScr;
    void Awake()
    {
        playerScr = PlayerObj.GetComponent<Player>();
    }
    void OnMouseOver()
    {
        playerScr.mouseOverHand = true;
        playerScr.mState = EPlayerState.eIdle;
    }

    public void TakeEgg()
    {
        playerScr.mState = EPlayerState.eWinn;
    }

    void OnMouseExit()
    {
        playerScr.mouseOverHand = false;
    }

    /*
    Rigidbody2D _rb;
    [SerializeField] GameObject PlayerObj;
    Player playerScr;
    Spline spline;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerScr = PlayerObj.GetComponent<Player>();
        spline = playerScr.spline;
    }
    private void Update()
    {
        if (playerScr.isDown && !playerScr.leanOn && !playerScr.hookedOn)
        {
            playerScr.spline.SetPosition(spline.GetPointCount() - 1, playerScr.mousePos - this.transform.position);
            Vector3 mousePosTop = new Vector3(playerScr.mousePos.x, playerScr.mousePos.y, playerScr.mousePos.z - 1);
            _rb.transform.position = mousePosTop;
        }

        if (spline != null && spline.GetPointCount() != 0)
        {
            Vector3 thisTranformPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);

            playerScr.pointLastPos = spline.GetPosition(spline.GetPointCount() - 1);
            _rb.transform.position = playerScr.pointLastPos + thisTranformPos;
        }
    }
    void OnMouseExit()
    {
        //playerScr.mouseOverHand = false;
    }
    */


}
