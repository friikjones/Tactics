using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject submenuSkills;

    public void CommandMove()
    {
        Debug.Log("Command Move");
    }
    public void CommandBasic()
    {
        Debug.Log("Command Basic");
    }
    public void CommandSkills()
    {
        Debug.Log("Command Skills");
        submenuSkills.SetActive(!submenuSkills.activeSelf);
    }
    public void CommandSkip()
    {
        Debug.Log("Command Skip");
    }
}
