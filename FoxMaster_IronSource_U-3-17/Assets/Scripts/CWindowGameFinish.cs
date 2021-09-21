using UnityEngine;
using UnityEngine.UI;

public class CWindowGameFinish : MonoBehaviour
{
   
    void Start()
    {
        GameObject panel = gameObject.transform.Find("BckgoundPanel").gameObject;

        GameObject buttonSelectObj = panel.transform.Find("ButtonSelect").gameObject;
        Button buttonSelect = buttonSelectObj.GetComponent<Button>();
        buttonSelect.onClick.AddListener(() => LevelSelectSceneButton());

        GameObject buttonNextObj = panel.transform.Find("ButtonNext").gameObject;
        Button buttonNext = buttonNextObj.GetComponent<Button>();
        buttonNext.onClick.AddListener(() => NextLevelButton());
    }

    public void NextLevelButton()
    {
        int aLevelID;
        aLevelID = CGameManager.Instance.mGameData.mActiveLevelId;
        CGameManager.Instance.SetLevelID(aLevelID+1);
        CGameManager.Instance.SwitchScene("GameScene");
        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
        //CGameController.instance.GetNotificationManager().Invoke(eventType);
    }

    public void LevelSelectSceneButton()
    {
        CGameManager.Instance.LoadScene("LevelSelectScene");

        Destroy(this.gameObject);
        Time.timeScale = 1.0f;
    }
}