using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSelectionUi : MonoBehaviour
{
    [SerializeField] List<GameObject> AllMenus;
    [SerializeField] PlayerCustomization pc;
    [SerializeField] TextMeshProUGUI MenuName;

    [SerializeField] List<Menus> BothMenus;
    [SerializeField] int MenuIndex;

    [System.Serializable]
    public class Menus
    {
        public GameObject MaleMenu;
        public GameObject FemaleMenu;
        public GameObject UiMenu;
        public string MenuName;
    }
    void Start()
    {
        MenuIndex= 0;
        OpenMenuByIndex(MenuIndex);
    }

    public void StopAllMenus()
    {
        foreach (var menu in AllMenus)
        {
            menu.SetActive(false);
        }
        foreach (var Menu in BothMenus)
        {
            Menu.UiMenu.SetActive(false);
        }
    }
    public void SwitchMenus()
    {
        OpenMenuByIndex(MenuIndex);
    }
    public void OpenMenuByIndex(int menuIdex)
    {
        StopAllMenus();
        if (pc.IsMale)
        {
            BothMenus[menuIdex].MaleMenu.SetActive(true);
           
        }
        else
        {
            BothMenus[menuIdex].FemaleMenu.SetActive(true);
        }
        MenuName.text = BothMenus[menuIdex].MenuName;
        BothMenus[menuIdex].UiMenu.SetActive(true);
    }
    public void GoToMenuLeft()
    {
        MenuIndex--;
        if (MenuIndex<0)
        {
            MenuIndex = BothMenus.Count-1;
        }
        OpenMenuByIndex(MenuIndex);
    }
    public void GoToMenuRight()
    {
        MenuIndex++;
        if (MenuIndex >= BothMenus.Count)
        {
            MenuIndex = 0;
        }
        OpenMenuByIndex(MenuIndex);
    }
}
