using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class GlobalMessageManager : MonoBehaviour
{
    public GlobalMessage GMessage;
    [Header("Uis properties")]
    public GameObject MessageUiHodler;
    public TextMeshProUGUI MessageTitle;
    public TextMeshProUGUI MessageMainText;
    public TextMeshProUGUI MessageItemCount;
    public Image ItemImg;

    [Space]
    [Header("Choise Options properties")]
    public bool IsInfo;
    public UnityEvent Yes_Event;
    public UnityEvent No_Event;

    [Space]
    [Header("Info message properties")]
    [SerializeField] GameObject InfoMessageUI;
    [SerializeField] GameObject OptionMessageUI;

    GameManager Gmanger;
    void Start()
    {
        Gmanger = GameManager.Instance;
    }

    public void SetGlobalMessage(GlobalMessage GM)
    {
        GMessage = GM;
        ShowMessage();
    }
    public void ShowMessage()
    {
        MessageTitle.text = GMessage.MessageTitle;
        MessageMainText.text= GMessage.MessageMainText;
        MessageUiHodler.SetActive(true);
        ItemImg.gameObject.SetActive(false);
        MessageItemCount.gameObject.SetActive(false);
        Gmanger.CanMove = false;
        if (GMessage.IsInfo)
        {
            if (GMessage.IsItem)
            {
                //get the item
                Gmanger.invetoryManager.TakeItemFromChest(GMessage.item, GMessage.Count);
                //-------

                InfoMessageUI.SetActive(true);
                OptionMessageUI.SetActive(false);
                MessageItemCount.gameObject.SetActive(true);
                MessageMainText.gameObject.SetActive(false);
                ItemImg.sprite = GMessage.item.DataHolder.ItemImg;
                ItemImg.gameObject.SetActive(true);
                MessageTitle.text ="You have "+ GMessage.item.DataHolder.ItemName;
                MessageItemCount.text ="x"+ GMessage.Count.ToString();
            }
            else
            {
                InfoMessageUI.SetActive(true);
                OptionMessageUI.SetActive(false);
                MessageMainText.gameObject.SetActive(true);
            }
         
        }
        else
        {
            MessageMainText.gameObject.SetActive(true);
            InfoMessageUI.SetActive(false);
            OptionMessageUI.SetActive(true);
        }
    }
    public void YesEvent()
    {
        if (GMessage!=null)
        {
            GMessage.SetYesEvent();
        }
        InfoMessageUI.SetActive(false);
        OptionMessageUI.SetActive(false);
        Gmanger.CanMove = true;
        MessageUiHodler.SetActive(false);
        Yes_Event?.Invoke();
    }

    public void NoEvent()
    {
        if (GMessage != null)
        {
            GMessage.SetNoEvent();
        }
        InfoMessageUI.SetActive(false);
        OptionMessageUI.SetActive(false);
        Gmanger.CanMove = true;
        MessageUiHodler.SetActive(false);
        No_Event?.Invoke();
    }
}
