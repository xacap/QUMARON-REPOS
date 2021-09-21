using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] public Vector2 longSight;
    [SerializeField] float __temp__offsetSight;

    [SerializeField] public List<Vector3> pointList = new List<Vector3>();
    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 directionVector;
    private float timer = 0;
    private float timeDelay = 0; 
    private int lastIndex = 0;
    private int endPointIndex = 0;
    private bool movingToEndList = true;
    private Rigidbody2D _rb;
    private BoxCollider2D boxCollider2D;
    private Vector3 V3right = new Vector3(1,0,0);
    private Vector2 offsetSight;
    Quaternion rotationLeft;
    Quaternion rotationRight;

    private void Awake()
    {
        endPointIndex = pointList.Count - 1;
        _rb = GetComponent<Rigidbody2D>();
        boxCollider2D = this.GetComponent<Transform>().GetComponent<BoxCollider2D>();
        rotationLeft = Quaternion.Euler(0, 0, 0);
        rotationRight = Quaternion.Euler(0, 180, 0);

        if (lastIndex >= 0 && lastIndex <= endPointIndex)
        {
            startPos = pointList[lastIndex];
            lastIndex++;

            endPos = pointList[lastIndex];
            lastIndex++;
        }
    }

    [System.Obsolete]
    void Update()
    {
        directionVector = endPos - startPos;
        directionVector = directionVector.normalized;
        timeDelay = time / (pointList.Count);
        this.transform.position = Vector3.Lerp(startPos, endPos, timer / timeDelay);

        // »ƒ≈Ã À≈¬Œ
        if (this.transform.position == endPos)
        {
            if (lastIndex >= 0 && lastIndex <= endPointIndex)
            {
                if (movingToEndList)
                {
                    startPos = endPos;
                    endPos = pointList[lastIndex];
                    lastIndex++;
                }
                else
                {
                    startPos = endPos;
                    endPos = pointList[lastIndex];
                    lastIndex--;
                }
            }
            timer = 0;
        }
        // »ƒ≈Ã À≈¬Œ *theEnd


        // »ƒ≈Ã œ–¿¬Œ
        if (this.transform.position == pointList[endPointIndex])
        {
            if (lastIndex < 0) lastIndex = 0; 

            if (lastIndex > pointList.Count - 1) lastIndex = pointList.Count - 1;

            if (lastIndex == endPointIndex)
            {
                movingToEndList = !movingToEndList;

                if (movingToEndList)
                {
                    endPointIndex = pointList.Count - 1;
                    lastIndex = pointList.Count - 2;
                }
                else
                {
                    endPointIndex = 0;
                    lastIndex = 1;
                }

                if (movingToEndList)
                {
                    startPos = endPos;
                    endPos = pointList[lastIndex];
                    lastIndex++;
                }
                else
                {
                    startPos = endPos;
                    endPos = pointList[lastIndex];
                    lastIndex--;
                }
            }

            if (!movingToEndList) timer = 0;
        }
        // »ƒ≈Ã œ–¿¬Œ *theEnd


        // –¿«¬Œ–Œ“
        if (directionVector.x <= V3right.x)
        {
            _rb.transform.rotation = rotationLeft;
        }

        if (directionVector.x >= V3right.x)
        {
            _rb.transform.rotation = rotationRight;
        }
        // –¿«¬Œ–Œ“ *theEnd


        // –¿«Ã≈– ¬«√Àﬂƒ¿
        boxCollider2D.size = longSight;
        float lS = longSight.x/ __temp__offsetSight;
        offsetSight.x = -lS;
        // –¿«Ã≈– ¬«√Àﬂƒ¿ *theEnd


        directionVector = startPos - endPos; // ¬≈ “Œ– Õ¿œ–¿¬À≈Õ»ﬂ


        //if (this.transform.position == pointList[endPointIndex] && this.transform.position == endPos)
        //{
        //    timer = 0;
        //}

        timer += Time.deltaTime;
    }
}

//Debug.Log("              |startPos: " + startPos);
