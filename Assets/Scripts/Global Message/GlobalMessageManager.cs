using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GlobalMessageManager : MonoBehaviour
{
    public GlobalMessage GMessage;
    [Header("Uis properties")]
    public GameObject MessageUiHodler;
    public TextMeshProUGUI MessageTitle;
    public TextMeshProUGUI MessageMainText;

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
        Gmanger.CanMove = false;
        if (GMessage.IsInfo)
        {
            InfoMessageUI.SetActive(true);
            OptionMessageUI.SetActive(false);
        }
        else
        {
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
