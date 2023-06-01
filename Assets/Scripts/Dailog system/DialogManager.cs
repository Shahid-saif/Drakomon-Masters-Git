using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogManager : MonoBehaviour
{
    [SerializeField] GameManager Gmanager;

    [Header("Dialog Properties")]
    [SerializeField] Dailog CurretnDialog;
    [SerializeField] int DialogIndex = 0;
    

    [Space]
    [Header("Dialog UIs")]
    public GameObject DialogUiHolder;
    [SerializeField] TextMeshProUGUI MainText;
    [SerializeField] TextMeshProUGUI NameText;
    [SerializeField] GameObject NameTextHolder;

    [Space]
    [Header("Dialog UIs OnScreen")]
    public GameObject OnScreenDialogUiHolder;
    [SerializeField] TextMeshProUGUI OnScreenMainText;
    [SerializeField] TextMeshProUGUI OnScreenNameText;
    [SerializeField] GameObject OnScreenNameTextHolder;


    #region Singleton
    public static DialogManager Dmanager;
    #endregion
    void Awake ()
    {
           Dmanager = this;
    }
    private void Start()
    {
        Gmanager = GameManager.Instance;
    }
    public void StartDialog()
    {
       
      
        DialogIndex = 0;
        if (CurretnDialog==null)
        {
            return;
        }
        Gmanager.InDialog = true;
        Gmanager.CanMove = false;
        Gmanager.PlayerControllerUiHolder.SetActive(false);
        CurretnDialog.StartEvent?.Invoke();

        if (CurretnDialog.OnScreen)
        {
            DialogUiHolder.SetActive(false);
            OnScreenDialogUiHolder.SetActive(true);
        }
        else
        {
            OnScreenDialogUiHolder.SetActive(false);
            DialogUiHolder.SetActive(true);
            DialogUiHolder.transform.position = CurretnDialog.DialogPosition.position;
        }

        if (CurretnDialog.HasNoName)
        {
            NameTextHolder.SetActive(false);
            OnScreenNameTextHolder.SetActive(false);
        }
        else
        {
            NameTextHolder.SetActive(true);
            OnScreenNameTextHolder.SetActive(true);
        }
        ShowSentences();
    }

    void ShowSentences()
    {
        if (CurretnDialog.OnScreen)
        {
            OnScreenMainText.text = CurretnDialog.Dialogs[DialogIndex].MainText;
            OnScreenNameText.text = CurretnDialog.Dialogs[DialogIndex].Name;
          
        }
        else
        {
            MainText.text = CurretnDialog.Dialogs[DialogIndex].MainText;
            NameText.text = CurretnDialog.Dialogs[DialogIndex].Name;
        }
      
        CurretnDialog.Dialogs[DialogIndex].EndSentencesEvetn?.Invoke();
    }

    public void EndDialog()
    {
        Gmanager.InDialog = false;
        Gmanager.CanMove = true;
        CurretnDialog.EndEvent?.Invoke();
        DialogUiHolder.SetActive(false);
        OnScreenDialogUiHolder.SetActive(false);
        Gmanager.PlayerControllerUiHolder.SetActive(true);
    }
    public void GoToNextSentence()
    {
        DialogIndex++;
        if (DialogIndex>=CurretnDialog.Dialogs.Count)
        {
            EndDialog();
            CurretnDialog = null;
        }
        else
        {
            ShowSentences();
        }
    }

    public void SetDialog(Dailog dialog)
    {
        CurretnDialog = dialog;
        StartDialog();
    }

}
