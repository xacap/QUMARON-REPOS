using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ActionSystem;


public class CPageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;
    public GameObject mRootPanel;
    public int currentPage = 1;

    private void Awake()
    {
        panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }

            StartCoroutine(SmoothMove(mRootPanel.transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }
    public void ButtonDownRight()
    {
        currentPage++;
        Vector3 newLocation = transform.position;
        newLocation += new Vector3(-Screen.width, 0, 0);
        StartCoroutine(SmoothMove(transform.position, newLocation, easing));
        panelLocation = newLocation;
    }

    public void ButtonDownLeft()
    {
        currentPage--;
        Vector3 newLocation = transform.position;
        newLocation += new Vector3(Screen.width, 0, 0);
        StartCoroutine(SmoothMove(mRootPanel.transform.position, newLocation, easing));
        panelLocation = newLocation;
    }

    IEnumerator MoveCoroutine(Vector3 startPosition, Vector3 targetPosition)
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, i);
            yield return null;
        }
        transform.position = targetPosition;
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
           

            yield return null;
        }
        CGameManager.Instance.GetNotificationManager().Invoke(EEventType.eChangeActivePage);
    }
}

