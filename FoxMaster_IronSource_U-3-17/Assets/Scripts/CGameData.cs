using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class CGameData
{
    private List<CLevelConfig> mLevelConfigsList;
    public List<CLevelConfig> ConfigsList
    {
        get { return mLevelConfigsList; } set { mLevelConfigsList = value; }
    }

    public int mActiveLevelId;

    //bool mIsAdsRemoved = false;
    private void Start()
    {
        ParseGameConfigs();
    }
    public void ParseGameConfigs()
    {
        CLevelSelectList tmpLevelSelectList = JsonUtility.FromJson<CLevelSelectList>(File.ReadAllText(Application.streamingAssetsPath + "/levelConfig.json"));
        mLevelConfigsList = tmpLevelSelectList.levelConfigList;
    }

   
    public void EditLevelConfigsList(int aLevelId)
    {
        for (int i = 0; i < mLevelConfigsList.Count; i++)
        {
            CLevelConfig config = mLevelConfigsList[i];

            if (config.levelID == aLevelId)
            {
                config.levelCompleted = true;
                mLevelConfigsList[i] = config;
            }
        }
    }
    public void SetActiveLevel(int levelID)
    {
        mActiveLevelId = levelID;
        EditLevelConfigsList(mActiveLevelId);
    }
    public bool isLevelFinished(int aLevelId)
    {
        for (int i = 0; i < mLevelConfigsList.Count; i++)
        {
            CLevelConfig config = mLevelConfigsList[i];
            if (config.levelID == aLevelId && config.levelCompleted == true)
            {
                return true;
            }
        }
        return false;
    }

    [Serializable]
    public class CLevelConfig
    {
        public int levelID;
        public string levelName;
        public bool levelCompleted = false;
    }
    public class CLevelSelectList
    {
        public List<CLevelConfig> levelConfigList;
    }
}
