using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelButton : MonoBehaviour
{
    [SerializeField] Font font;
    [SerializeField] Sprite levelCompletedSprite;

    [System.Obsolete]
    public void Initialize(int mLevelID, string mLevelName, bool mLevelCompleted, bool buttonActive, GameObject ImageLvlCompltd)
    {
        this.GetComponent<Button>().interactable = buttonActive;
        this.GetComponent<Button>().transition = Selectable.Transition.SpriteSwap;

        if (mLevelCompleted)
        {
            this.GetComponent<Button>().image.sprite = levelCompletedSprite;
            this.transform.GetChild(2).gameObject.active = true;
            this.transform.GetChild(1).gameObject.active = true;
            this.transform.GetChild(0).gameObject.active = true;
        }
        else 
        {
            this.transform.GetChild(2).gameObject.active = false;
            this.transform.GetChild(1).gameObject.active = false;
            this.transform.GetChild(0).gameObject.active = false;
        }
       
        GameObject newTextID = new GameObject("Text level ID ", typeof(Text));
        newTextID.transform.SetParent(this.transform);

        string scnIDstring;
        scnIDstring = mLevelID.ToString();
        newTextID.GetComponent<Text>().text = scnIDstring;
        newTextID.GetComponent<Text>().font = font;
        newTextID.GetComponent<Text>().fontStyle = FontStyle.Bold;
        newTextID.GetComponent<Text>().fontSize = 60;
        newTextID.GetComponent<Text>().color = new Color(255, 244, 208);
        Shadow shadow = newTextID.AddComponent(typeof(Shadow)) as Shadow;
        Color shadowColor = new Color(0,0,0);
        shadowColor.a = 0.25f;
        shadow.effectColor = shadowColor;
        shadow.effectDistance = new Vector2(1.6f, -12f);
        newTextID.GetComponent<Text>().alignment = TextAnchor.UpperCenter;

        RectTransform rt = newTextID.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(0, 80);

        this.GetComponent<Button>().onClick.AddListener(delegate {
            CGameManager.Instance.SetLevelID(mLevelID);
            CGameManager.Instance.SwitchScene(mLevelName);
        });
    }
}
