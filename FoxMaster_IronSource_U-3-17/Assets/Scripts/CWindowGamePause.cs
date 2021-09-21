using UnityEngine;
using UnityEngine.UI;


public class CWindowGamePause : MonoBehaviour
{
    [SerializeField] GameObject panelPause;

    void Start()
    {
        GameObject buttonMainMenuObj = panelPause.transform.Find("ButtonMainMenu").gameObject;
        Button buttonMainMenu = buttonMainMenuObj.GetComponent<Button>();
        buttonMainMenu.onClick.AddListener(() => MainMenu());

        GameObject buttonRestartObj = panelPause.transform.Find("ButtonRestart").gameObject;
        Button buttonRestart = buttonRestartObj.GetComponent<Button>();
        buttonRestart.onClick.AddListener(() => Restart());

        GameObject buttonContinuetObj = panelPause.transform.Find("ButtonContinue").gameObject;
        Button buttonContinue = buttonContinuetObj.GetComponent<Button>();
        buttonContinue.onClick.AddListener(() => Continue());
    }

    public void MainMenu()
    {
        //Destroy(this.gameObject);

        Debug.Log("MainMenu");
    }

    public void Restart()
    {
        //CGameManager.Instance.SetLevelID(uLevelID);
        //CGameManager.Instance.SwitchScene(uLevelName);

        //Time.timeScale = 1.0f;
        //Destroy(this.gameObject);

        Debug.Log("Restart");

    }

    public void Continue()
    {
        //Time.timeScale = 1.0f;
        //Destroy(this.gameObject);

        Debug.Log("Continue");

    }
}
