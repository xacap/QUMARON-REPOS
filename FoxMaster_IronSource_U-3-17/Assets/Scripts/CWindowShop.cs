using UnityEngine;
using UnityEngine.UI;


public class CWindowShop : MonoBehaviour
{
    [SerializeField] GameObject panelShop;


    void Start()
    {
        GameObject sButtonTopObj = panelShop.transform.Find("SButtonTop").gameObject;
        Button sButtonTop = sButtonTopObj.GetComponent<Button>();
        sButtonTop.onClick.AddListener(() => TopSDisableADS());

        GameObject sButtonCenterObj = panelShop.transform.Find("SButtonCenter").gameObject;
        Button sButtonCenter = sButtonCenterObj.GetComponent<Button>();
        sButtonCenter.onClick.AddListener(() => CenterSDisableADS());

        GameObject sButtonBottomObj = panelShop.transform.Find("SButtonBottom").gameObject;
        Button sButtonBottom = sButtonBottomObj.GetComponent<Button>();
        sButtonBottom.onClick.AddListener(() => BottomSDisableADS());


        GameObject wideoButtonTopObj = panelShop.transform.Find("WideoButtonTop").gameObject;
        Button wideoButtonTop = wideoButtonTopObj.GetComponent<Button>();
        wideoButtonTop.onClick.AddListener(() => TopWideoDisableADS());

        GameObject wideoButtonCenterObj = panelShop.transform.Find("WideoButtonCenter").gameObject;
        Button wideoButtonCenter = wideoButtonCenterObj.GetComponent<Button>();
        wideoButtonCenter.onClick.AddListener(() => CenterWideoDisableADS());

        GameObject wideoButtonBottomObj = panelShop.transform.Find("WideoButtonBottom").gameObject;
        Button wideoButtonBottom = wideoButtonBottomObj.GetComponent<Button>();
        wideoButtonBottom.onClick.AddListener(() => BottomWideoDisableADS());
    }

    public void TopSDisableADS()
    {
        Debug.Log("TopSDisableADS");
    }

    public void CenterSDisableADS()
    {
        Debug.Log("CenterSDisableADS");
    }

    public void BottomSDisableADS()
    {
        Debug.Log("BottomSDisableADS");
    }

    public void TopWideoDisableADS()
    {
        Debug.Log("TopWideoDisableADS");
    }

    public void CenterWideoDisableADS()
    {
        Debug.Log("CenterWideoDisableADS");
    }

    public void BottomWideoDisableADS()
    {
        Debug.Log("BottomWideoDisableADS");
    }

    public void CloseThisWindow()
    {
        Destroy(gameObject);
    }
}
