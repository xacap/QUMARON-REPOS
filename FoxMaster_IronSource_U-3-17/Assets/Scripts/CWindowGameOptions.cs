using UnityEngine;
using UnityEngine.UI;


public class CWindowGameOptions : MonoBehaviour
{
    [SerializeField] GameObject panelOption;
        
    void Start()
    {
        GameObject buttonTermsObj = panelOption.transform.Find("ButtonTermsOfUse").gameObject;
        Button buttonTermsOfUse = buttonTermsObj.GetComponent<Button>();
        buttonTermsOfUse.onClick.AddListener(() => LinkTermsOfUse());

        GameObject buttonPrivacyObj = panelOption.transform.Find("ButtonPrivacyPolicy").gameObject;
        Button buttonPrivacyPolicy = buttonPrivacyObj.GetComponent<Button>();
        buttonPrivacyPolicy.onClick.AddListener(() => LinkPrivacyPolicy());
    }

    public void LinkTermsOfUse()
    {
        Application.OpenURL("https://qumaron.com/terms-of-service");
    }

    public void LinkPrivacyPolicy()
    {
        Application.OpenURL("https://qumaron.com/privacy-policy");
    }

    public void CloseThisWindow()
    {
        Destroy(gameObject);
    }
}
