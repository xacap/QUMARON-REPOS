using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using static CGameData;
using ActionSystem;


public class CLevelSelectScene : MonoBehaviour
{
    Camera mCamera;
    bool panelIsChange;

    GameObject lvlCompltdImage;
    List<CLevelConfig> levelConfigsList;
    List<GameObject> mIndicatorList = new List<GameObject>();

    [SerializeField] GameObject rootPanel;
    [SerializeField] GameObject levelIcon;
    [SerializeField] GameObject thisCanvas;
    [SerializeField] GameObject CircleIndicator;
    [SerializeField] GameObject panelIndicators;
    [SerializeField] GameObject buttonLeft;
    [SerializeField] GameObject buttonRight;
    private Vector2 iconSpacing;
    CPageSwiper mCPageSwiper;
    public int mCurrentPage;

    private int indexActivePanel;
    private int numberOfLevels;
    
    private Rect panelSizes;
    private Rect iconSizes;
    private int amountPerPage;
    private int currentLevelCount = 0;


    private void Awake()
    {
        levelConfigsList = CGameManager.Instance.mGameData.ConfigsList;
        numberOfLevels = levelConfigsList.Count();
        mCamera = UnityEngine.Camera.main;
        CGameManager.Instance.GetNotificationManager().Register(EEventType.eChangeActivePage, IsChangeIndicator);
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    [System.Obsolete]
    private void Start()
    {
        panelSizes = rootPanel.GetComponent<RectTransform>().rect;
        iconSizes = levelIcon.GetComponent<RectTransform>().rect;
        int maxInARow = 5;
        int maxInACol = 5;
        amountPerPage = maxInARow * maxInACol;
        int totalPages = Mathf.CeilToInt((float)numberOfLevels / amountPerPage);
        LoadPanels(totalPages);
        //mIndicatorList.Add(fakeIndicator);
    }


    [System.Obsolete]
    void LoadPanels(int numberOfPanels)
    {
        GameObject panelClone = Instantiate(rootPanel) as GameObject;
        mCPageSwiper = rootPanel.AddComponent<CPageSwiper>();
        mCPageSwiper.totalPages = numberOfPanels;
        mCPageSwiper.mRootPanel = rootPanel;

        for (int i = 1; i <= numberOfPanels; i++)
        {
            GameObject panel = Instantiate(panelClone) as GameObject;
            panel.transform.SetParent(thisCanvas.transform, false);
            panel.transform.SetParent(rootPanel.transform);
            panel.name = "Page-" + i;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(panelSizes.width * (i - 1), 0);
                        
            if (i <= 1)
            {
                indexActivePanel = i;
            }

            LoadCircleIndicator(i);

            SetUpGrid(panel);

            int numberOfIcons = i == numberOfPanels ? numberOfLevels - currentLevelCount : amountPerPage;
            LoadIcons(numberOfIcons, panel);
        }
        Destroy(panelClone);
    }

    [System.Obsolete]
    private void Update()
    {
        mCurrentPage = mCPageSwiper.currentPage;

        if (mCurrentPage <= 1)
        {
            buttonLeft.active = false;
        }
        else
        {
            buttonLeft.active = true;
        }

        if (mCurrentPage >= mCPageSwiper.totalPages)
        {
            buttonRight.active = false;
        }
        else
        {
            buttonRight.active = true;
        }
    }

    void LoadCircleIndicator(int numberPages)
    {
        GameObject indicator = Instantiate(CircleIndicator) as GameObject;
        indicator.transform.SetParent(thisCanvas.transform, false);
        indicator.transform.SetParent(panelIndicators.transform);
        indicator.GetComponent<CIndicator>().indicatorID = numberPages;

        mIndicatorList.Add(indicator);

        if (indexActivePanel == numberPages)
        {
            indicator.GetComponent<CIndicator>().indicatorCondition = true;
        }
    }
    public void IsChangeIndicator()
    {
        foreach (GameObject indictr in mIndicatorList)
        {
            if (indictr != null)
            {
                if (indictr.GetComponent<CIndicator>().indicatorID == mCurrentPage)
                {
                    indictr.GetComponent<CIndicator>().indicatorCondition = true;
                }
                else
                {
                    indictr.GetComponent<CIndicator>().indicatorCondition = false;
                }
            }
            else
            {
                Debug.Log("indictr == null");
            }
            
        }
    }

    public void ButtonLeft()
    {
        mCPageSwiper.ButtonDownLeft();
    }
    public void ButtonRight()
    {
        mCPageSwiper.ButtonDownRight();
    }

    [System.Obsolete]
    void LoadIcons(int numbIcons, GameObject parentObject)
    {
        int aLevelID;
        string aLevelName;
        bool aLevelCompleted;

        for (int i = 0; i < numbIcons; i++)
        {
            aLevelID = levelConfigsList[currentLevelCount].levelID;
            aLevelName = levelConfigsList[currentLevelCount].levelName;
            aLevelCompleted = levelConfigsList[currentLevelCount].levelCompleted;
            bool buttonActive = false;

            if ((currentLevelCount - 1) < 0)
            {
                buttonActive = true;
            }
            else if (levelConfigsList[currentLevelCount - 1].levelCompleted == true)
            {
                buttonActive = true;
            }

            GameObject icon = Instantiate(levelIcon) as GameObject;
            icon.transform.SetParent(thisCanvas.transform, false);
            icon.transform.SetParent(parentObject.transform);

            CLevelButton aLevelIcon = icon.GetComponent<CLevelButton>();
            aLevelIcon.Initialize(aLevelID, aLevelName, aLevelCompleted, buttonActive, lvlCompltdImage);

            currentLevelCount++;
        }
    }
    void SetUpGrid(GameObject panel)
    {
        GridLayoutGroup grid = panel.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(166f, 166f);
        grid.spacing = new Vector2 (14.4f, 44f);
        grid.startCorner = GridLayoutGroup.Corner.UpperLeft;
        grid.startAxis = GridLayoutGroup.Axis.Horizontal;
        grid.childAlignment = TextAnchor.UpperCenter;
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 5;
    }

    public void LoadPreviousLevel()
    {
        SceneManager.LoadSceneAsync("StartScene");
    } 
}
