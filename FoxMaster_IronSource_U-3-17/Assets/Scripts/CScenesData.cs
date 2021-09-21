using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "sceneDB", menuName = "Scene Data/Database")]
public class ScenesData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public List<Menu> menus = new List<Menu>();
    public int CurrentLevelIndex = 1;

    /*
 	* ������
 	*/

    // ��������� C���� C �������� �����C��
    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            // ��������� C���� �������� ��� ������
            SceneManager.LoadSceneAsync("Gameplay" + index.ToString());
            // ��������� ������ ��C�� ������ � ���������� ������
            SceneManager.LoadSceneAsync("Level" + index.ToString() + "Part1", LoadSceneMode.Additive);
        }
        // C���C����� �����C, �C�� � ��C ������ ��� �������
        else CurrentLevelIndex = 1;
    }
    // ����C� C��������� ������
    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    // ��������C���� ������� �������
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    // ����� ����, �������� ������� ������
    public void NewGame()
    {
        LoadLevelWithIndex(1);
    }

    /*
 	* ����
    */

    // ��������� ������� ����
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].sceneName);
    }
    // ��������� ���� �����
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].sceneName);
    }
}
