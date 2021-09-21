using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGUIManager : MonoBehaviour
{
    public void ShowGameFinishWindow()
    {
        GameObject go = Instantiate(Resources.Load("CanvasFinishWindow")) as GameObject;
        go.GetComponent<RectTransform>().localPosition = new Vector3(960f, 540f, 0);
    }
}
