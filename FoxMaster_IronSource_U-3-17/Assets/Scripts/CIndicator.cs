using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CIndicator : MonoBehaviour
{
    public int indicatorID;
    public bool indicatorCondition;
    [SerializeField] Sprite indicatorActiveSprite;
    [SerializeField] Sprite indicatorPassiveSprite;

    void Update()
    {
        if (indicatorCondition == true)
        {
            GetComponent<Image>().sprite = indicatorActiveSprite;
        }
        else
        {
            GetComponent<Image>().sprite = indicatorPassiveSprite;
        }
    }
}
