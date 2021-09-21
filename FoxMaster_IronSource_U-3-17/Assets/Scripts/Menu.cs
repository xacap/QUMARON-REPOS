using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Main_Menu,
    Pause_Menu
}

[CreateAssetMenu(fileName = "NewMenu", menuName = "Scene Data/Menu")]
public class Menu : GameScene
{
    // НаCтройки, отноCящиеCя только к меню
    [Header("Menu specific")]
    public Type type;
}
