using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static CGameData;


public class CLevelStartScene : MonoBehaviour
{
    [SerializeField] GameObject optionWindow;
    
    public void TestVideo()
    {
        CGameManager.Instance.mAdsManager.playVideo();
    }
    public void LoadLevelSelectScene()
    {
        SceneManager.LoadSceneAsync("LevelSelectScene");
    }
    public void LoadOptionWindow()
    {
        Instantiate(optionWindow, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void LoadAvailableScene()
    {
        List<CLevelConfig> uLevelConfigsList = CGameManager.Instance.mGameData.ConfigsList;

        int uLevelID;
        string uLevelName;
        bool uLevelCompleted;

        for (int i = 0; i < uLevelConfigsList.Count; i++)
        {
            uLevelID = uLevelConfigsList[i].levelID;
            uLevelName = uLevelConfigsList[i].levelName;
            uLevelCompleted = uLevelConfigsList[i].levelCompleted;

            if (uLevelCompleted == false && uLevelID == 1)
            {

                CGameManager.Instance.SetLevelID(uLevelID);
                CGameManager.Instance.SwitchScene(uLevelName);

                return;
            }

            if (i > 0)
            {
                if (uLevelConfigsList[i-1].levelCompleted == true && uLevelCompleted == false)
                {

                    CGameManager.Instance.SetLevelID(uLevelID);
                    CGameManager.Instance.SwitchScene(uLevelName);

                    return;
                }
            }
        }
    }
    void OnApplicationPause(bool isPaused)
    {
        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
        IronSource.Agent.onApplicationPause(isPaused);
    }
}

