using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "Scene Data/Level")]
public class Level : GameScene
{
    // ��C������, ����C����C� ������ � ������
    [Header("Level specific")]
    public int enemiesCount;
}
