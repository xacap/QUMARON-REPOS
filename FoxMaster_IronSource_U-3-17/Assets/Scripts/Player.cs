using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using System;
using ActionSystem;
using UnityEngine.UI;

public enum EPlayerState
{
    eIdle,
    eShape,
    eArmReturn,
    eWinn
}

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("Player");
                    instance = instanceContainer.AddComponent<Player>();
                }
            }
            return instance;
        }
    }
    private static Player instance;

    public List<GameObject> ColliderList = new List<GameObject>();
    public string Web { get; private set; }
    public string Fence { get; private set; }

    public EPlayerState mState;

    [SerializeField] float minimumDistance;
    [SerializeField] float pointRemoveDelay;
    //[SerializeField] int countPoint = 3;
    [SerializeField] GameObject Arm;
    [SerializeField] GameObject ArmSprite;
    private Animator animator;
    public Vector3 mousePos;
    private Vector3 lastPosition;
    private Spline spline;
    [SerializeField] bool cycleOn = false;
    [SerializeField] public bool isDown = false;
    [SerializeField] public bool leanOn = false;
    //[SerializeField] public bool hookedOn = false;
    //[SerializeField] bool removePoint = false;
    [SerializeField] bool touchedWeb = false;
    [SerializeField] float mTime;
    [SerializeField] ShapeTangentMode mBroken;
    [SerializeField] ShapeTangentMode mContinuous;

    public Vector3 pointLastPos = Vector3.zero;
    //private Vector3 colliderSize;
    private Vector3 colliderCenter;
    private Vector3 mouseTransformPos;
    private Vector3 pointV31 = Vector3.zero;
    private Vector3 pointV32 = Vector3.zero;
    private SpriteShapeController spriteShapeController;
    Vector2 currentMousePos;
    [SerializeField] LayerMask layerMask;

    public bool mouseOverHand = false;
    public bool pointCreated = false;

    Vector3 lastPoint;
    Vector3 lastButOnePoint;
    [SerializeField] int splinePontIndex = 3;

    public GameObject mThisObject;
    EdgeCollider2D mThisCollider;
    Collider2D mStopCollider;

    bool crossingMyself = false;

    [SerializeField] GameObject checkPont;
    CircleCollider2D checkPontCollider;

    private void Awake()
    {
        mState = EPlayerState.eIdle;
        spriteShapeController = gameObject.GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;
        mThisObject = this.gameObject;
        //mThisCollider = mThisObject.GetComponent<EdgeCollider2D>();
        //checkPontCollider = checkPont.GetComponent<CircleCollider2D>();
        animator = ArmSprite.GetComponent<Animator>();
    }

    void Start()
    {
        if (mThisObject != null)
            mThisCollider = mThisObject.GetComponent<EdgeCollider2D>();

        if (checkPont != null)
            checkPontCollider = checkPont.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        currentMousePos = mousePos;

        MouseTransformPosition();

        if (ColliderList.Count > 0)
        {
            if (mouseOverHand && isDown)
            {
                mStopCollider = Physics2D.OverlapPoint(currentMousePos, layerMask);

                for (int i = 0; i < ColliderList.Count; i++)
                {
                    if (!mStopCollider) continue;

                    if (mThisCollider.bounds.Intersects(mStopCollider.bounds))
                    {
                        leanOn = true;
                    }

                    if (mStopCollider == ColliderList[i].GetComponent<BoxCollider2D>() ||
                        mStopCollider == ColliderList[i].GetComponent<CapsuleCollider2D>() ||
                        mStopCollider == ColliderList[i].GetComponent<CircleCollider2D>())
                    {
                        leanOn = true;

                        if (ColliderList[i].transform.CompareTag("web"))
                        {
                            if (touchedWeb)
                            {
                                if ((mTime + 2) <= Time.time)
                                {
                                    animator.SetBool("RoseAllert", false);
                                    mState = EPlayerState.eArmReturn;
                                    animator.SetBool("RedAllert", true);
                                    if (mouseOverHand || !mouseOverHand) Drop();
                                    touchedWeb = false;
                                    return;
                                }
                            }
                            else if (spline != null && spline.GetPointCount() >= 3)
                            {
                                spline.RemovePointAt(spline.GetPointCount() - 1);
                                mState = EPlayerState.eIdle;
                                animator.SetBool("RoseAllert", true);
                                Vector3 lastPointPos = spline.GetPosition(spline.GetPointCount() - 1) + this.transform.position;
                                lastPointPos = lastPointPos - Vector3.forward;
                                Arm.transform.position = lastPointPos;
                                mTime = Time.time;
                                touchedWeb = true;
                                return;
                            }
                        }

                        if (ColliderList[i].transform.CompareTag("lamp"))
                        {
                            if (touchedWeb)
                            {
                                if (mTime + 2 <= Time.time)
                                {
                                    animator.SetBool("RoseAllert", false);
                                    mState = EPlayerState.eArmReturn;
                                    animator.SetBool("RedAllert", true);
                                    Drop();
                                    touchedWeb = false;
                                    return;
                                }
                            }
                            else if (spline != null && spline.GetPointCount() >= 3)
                            {
                                spline.RemovePointAt(spline.GetPointCount() - 1);
                                mState = EPlayerState.eIdle;
                                animator.SetBool("RoseAllert", true);
                                Vector3 lastPointPos = spline.GetPosition(spline.GetPointCount() - 1) + this.transform.position;
                                lastPointPos = lastPointPos - Vector3.forward;
                                Arm.transform.position = lastPointPos;
                                mTime = Time.time;
                                touchedWeb = true;
                                return;
                            }
                        }

                        return;
                    }

                    leanOn = false;
                }
            }
        }

        if (isDown && !leanOn) // транCформ позиции ладошки
        {

            spline.SetPosition(spline.GetPointCount() - 1, mousePos - this.transform.position);
            Vector3 mousePosTop = new Vector3(mousePos.x, mousePos.y, mousePos.z - 1);
            Arm.transform.position = mousePosTop;
            lastButOnePoint = spline.GetPosition(spline.GetPointCount() - 2);
            lastPoint = spline.GetPosition(spline.GetPointCount() - 1);

            Vector3 relativePos = lastPoint - lastButOnePoint;

            Quaternion rotation = Quaternion.LookRotation(relativePos.normalized, -Vector3.forward);
            Quaternion rotation00 = new Quaternion(0, 0, 0, 0);

            if (lastPoint.x > lastButOnePoint.x || lastPoint.x > lastButOnePoint.x)
            {
                rotation00.z = rotation.x;
            }

            if (lastPoint.y > lastButOnePoint.y)
            {
                rotation00.z = rotation.y;
            }

            rotation00.w = rotation.w;

            Arm.transform.rotation = rotation00;

        }

        DistanceBethPointsAndRemouve();

        /*checkPont.transform.position = spline.GetPosition(spline.GetPointCount() - 1) + this.transform.position + Vector3.right/3;

        if (mThisCollider.bounds.Intersects(checkPontCollider.bounds))
        {
            crossingMyself = true;
            Debug.Log("# crossingMyself = true;");
        }*/

    }

    private void MouseTransformPosition()
    {
        if (leanOn)
        {
            Vector3 touchTriggerPos = Input.mousePosition;
            touchTriggerPos = Camera.main.ScreenToWorldPoint(touchTriggerPos);

            if (lastPosition.x < colliderCenter.x)
            {
                if (lastPosition.x < touchTriggerPos.x)
                {
                    leanOn = true;
                }
                else leanOn = false;
            }
            else if (lastPosition.x > colliderCenter.x)
            {
                if (lastPosition.x > touchTriggerPos.x)
                {
                    leanOn = true;
                }
                else leanOn = false;
            }
            else if (lastPosition.y < colliderCenter.y)
            {
                if (lastPosition.y < touchTriggerPos.y)
                {
                    leanOn = true;
                }
                else leanOn = false;
            }
            else if (lastPosition.y > colliderCenter.y)
            {
                if (lastPosition.y > touchTriggerPos.y)
                {
                    leanOn = true;
                }
                else leanOn = false;
            }
        }

        mousePos = Input.mousePosition;
        mousePos = mousePos - this.transform.position;
        mousePos.z = 10.0f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //float distanceForAddPoint = Mathf.Abs((mousePos - lastPosition).magnitude);

        if (mouseOverHand && isDown && !leanOn && mState != EPlayerState.eArmReturn/* && distanceForAddPoint > minimumDistance*/)
        {
            //AddPoint(mousePos, true);
            Vector3 mousePosTop = new Vector3(mousePos.x, mousePos.y, mousePos.z - 1);
            Arm.transform.position = mousePosTop;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("AncPoint"))
        {
            if (!pointCreated && isPossibleAddPoint(collider.transform.position))
            {
                spline.SetPosition(spline.GetPointCount() - 1, collider.transform.position - this.transform.position);
                Vector3 pos = collider.transform.position;
                pos.x += 0.1f;
                AddPoint(pos, true);
                pointCreated = true;
            }
        }
    }
    private void AddPoint(Vector3 pointPos, bool aNeedPlayerTransform = true)
    {
        if (aNeedPlayerTransform)
        {
            spline.InsertPointAt(spline.GetPointCount(), pointPos - this.transform.position);
        }
        else
        {
            spline.InsertPointAt(spline.GetPointCount(), pointPos);
        }

        int newPointIndex = spline.GetPointCount() - 1;
        spline.SetTangentMode(newPointIndex, ShapeTangentMode.Continuous);
        spline.SetHeight(newPointIndex, 1.0f);
        lastPosition = pointPos;
    }
    private bool isPossibleAddPoint(Vector3 pointCheck)
    {
        float differenceDist = 0.4f;

        for (int i = 0; i < spline.GetPointCount() - 2; i++)
        {
            float dist = Vector3.Distance(pointCheck, spline.GetPosition(i));

            if (dist <= differenceDist)
            {
                return false;
            }
        }
        return true;
    }

    /*private void IsPossibleAddPoint(Vector3 pointCheck)
    {
        float differenceDist = 0.4f;

        for (int i = 0; i < spline.GetPointCount() - 2; i++)
        {
            float dist = Vector3.Distance(pointCheck, spline.GetPosition(i));

            if (dist >= differenceDist)
            {
                isPossibleAddPoint = true; 
            }
            else isPossibleAddPoint = false;
        }
    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.CompareTag("enemy"))
        {
            mState = EPlayerState.eArmReturn;
            Drop();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.transform.CompareTag("web"))
        {
            leanOn = false;
        }
    }

    void ArmMoveAndReturn()
    {
        if (spline != null && spline.GetPointCount() != 0)
        {
            Vector3 thisTranformPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1);

            pointLastPos = spline.GetPosition(spline.GetPointCount() - 1);

            Arm.transform.position = pointLastPos + thisTranformPos;
        }
    }
    private void CheckCollider()
    {
        Vector2 currentMousePos = mousePos;

        for (int i = 0; i < ColliderList.Count; i++)
        {
            if (Physics2D.OverlapPoint(currentMousePos) == ColliderList[i].GetComponent<BoxCollider2D>())
            {
                mousePos = currentMousePos;
            }
        }
    }
    public void Down()
    {
        if (mouseOverHand)
        {
            isDown = true;
            AddPoint(mousePos);
        }
    }
    public void Drop()
    {
        isDown = false;
        mouseOverHand = false;
        cycleOn = true;
        StartCoroutine(IERemovePoint());
    }
    void DistanceBethPointsAndRemouve()
    {
        lastPoint = spline.GetPosition(spline.GetPointCount() - 1);
        lastButOnePoint = spline.GetPosition(spline.GetPointCount() - 2);
        float distance = Vector2.Distance(lastPoint, lastButOnePoint);

        if (pointCreated)
        {
            if (distance >= 0.48f)
            {
                pointCreated = false;
            }
        }
        else
        {
            if (spline != null && spline.GetPointCount() >= 4 && distance < 0.3f)
            {
                spline.RemovePointAt(spline.GetPointCount() - 2);
            }
        }
    }
    IEnumerator IERemovePoint()
    {
        while (cycleOn)
        {
            yield return null;

            if (spline != null && spline.GetPointCount() >= 3)
            {
                for (int i = 0; i < spline.GetPointCount(); i++)
                {
                    spline.RemovePointAt(spline.GetPointCount() - 1);
                    ArmMoveAndReturn();
                    yield return new WaitForSeconds(pointRemoveDelay);
                }
                animator.SetBool("RedAllert", false);
            }
            else
            {
                cycleOn = false;


                if (mState == EPlayerState.eWinn)
                {
                    CGameManager.Instance.GetNotificationManager().Invoke(EEventType.eRestartGameEvent);
                }

                mState = EPlayerState.eIdle;
                Arm.transform.rotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
    /*IEnumerator IERubberHand()
    {
        for (int i = 0; i < spline.GetPointCount(); i++)
        {
            spline.SetTangentMode((spline.GetPointCount() - 1 - i), (ShapeTangentMode)mBroken);
        }
        yield return new WaitForSeconds(0.5f);

        for (int i = 1; i < spline.GetPointCount(); i++)
        {
            spline.SetTangentMode((spline.GetPointCount() - 1 - i), (ShapeTangentMode)mContinuous);
        }
    }*/


    /*private void AddPoint(Vector3 pointPos, bool aNeedPlayerTransform = true)
       {

           if (aNeedPlayerTransform)
           {
               spline.InsertPointAt(spline.GetPointCount(), pointPos - this.transform.position);
           }
           else
           {
               spline.InsertPointAt(spline.GetPointCount(), pointPos);
           }

           int newPointIndex = spline.GetPointCount() - 1;

           spline.SetTangentMode(newPointIndex, ShapeTangentMode.Continuous);
           spline.SetHeight(newPointIndex, 1.0f);

           lastPosition = pointPos;

       }*/

}

