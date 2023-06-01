using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [SerializeField] GameManager Gmanager;
    [Header("Quest properties")]
    public List<Quest_DataHolder> AllQuests;

    [Space]
    [Header("UIs Helper")]
    [SerializeField] Transform Quest_UI_parent;
    [SerializeField] GameObject QuestbuttonUI_Template;
    [SerializeField] List<GameObject> Quest_UI_Elements;

    [Space]
    [Header("Quest Ui properties")]
    public GameObject QuestUIHolder;
    public TextMeshProUGUI QuestName;
    public TextMeshProUGUI QuestDescription;

    #region Singleton
    public static QuestManager Instance;
    #endregion
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Gmanager = GameManager.Instance;

        SetUpAllQuests();
        QuestbuttonUI_Template.SetActive(false);
    }

    public void ClearQuest_UI_Elements()
    {
        foreach (var Element in Quest_UI_Elements)
        {
            Destroy(Element);
        }
    }
    public void SetUpAllQuests()
    {
        ClearQuest_UI_Elements();
        Quest_UI_Elements.Clear();
        foreach (var Quest in AllQuests)
        {
            Quest.SaveName = Quest.Id + Quest.QuestName;
            Quest.IsCompleted_Data = PlayerPrefs.GetInt(Quest.SaveName + "_IsCompleted_Data", 0);
            Quest.IsActive_Data = PlayerPrefs.GetInt(Quest.SaveName + "_IsActive_Data", 0);
            if (Quest.IsCompleted_Data > 0)
            {
                Quest.IsCompleted = true;
            }
            else
            {
                Quest.IsCompleted = false;
            }
            if (Quest.IsActive_Data > 0)
            {
                Quest.IsActive = true;
            }
            else
            {
                Quest.IsActive = false;
            }

            //setup ui 

            if (Quest.IsActive && !Quest.IsCompleted)
            {
                GameObject obj = Instantiate(QuestbuttonUI_Template, Quest_UI_parent);
                //  obj.transform.position = QuestbuttonUI_Template.transform.position;
                if (obj != null)
                {
                    obj.SetActive(true);
                    QuestViewer QV = obj.GetComponent<QuestViewer>();
                    if (QV != null)
                    {
                        QV.Quest = Quest;
                        QV.QuestButtonName.text = Quest.QuestName;
                        Quest_UI_Elements.Add(obj);
                        QV.OnProgressImg.SetActive(true);
                        QV.OnCompleteImg.SetActive(false);
                    }
                }

            }
        }
        foreach (var Quest in AllQuests)
        {
            if (Quest.IsCompleted)
            {
                PlayerPrefs.SetInt(Quest.SaveName + "_IsActive_Data", 0);
                GameObject obj = Instantiate(QuestbuttonUI_Template, Quest_UI_parent);
                //  obj.transform.position = QuestbuttonUI_Template.transform.position;
                if (obj != null)
                {
                    obj.SetActive(true);
                    QuestViewer QV = obj.GetComponent<QuestViewer>();
                    if (QV != null)
                    {
                        QV.Quest = Quest;
                        QV.QuestButtonName.text = Quest.QuestName + " Completed";
                        Quest_UI_Elements.Add(obj);
                    }
                    QV.OnProgressImg.SetActive(false);
                    QV.OnCompleteImg.SetActive(true);
                    Button B = obj.GetComponent<Button>();
                    if (B != null)
                    {
                        B.interactable = false;
                        B.onClick.RemoveAllListeners();
                    }
                }
            }
        }
    }
    public void RestSaveQusestsData_Debug()
    {
        foreach (var Quest in AllQuests)
        {
            Quest.SaveName = Quest.Id + Quest.QuestName;
            PlayerPrefs.SetInt(Quest.SaveName + "_IsCompleted_Data", 0);
            PlayerPrefs.SetInt(Quest.SaveName + "_IsActive_Data", 0);
            if (Quest.IsCompleted_Data > 0)
            {
                Quest.IsCompleted = true;
            }
            else
            {
                Quest.IsCompleted = false;
            }
            if (Quest.IsActive_Data > 0)
            {
                Quest.IsActive = true;
            }
            else
            {
                Quest.IsActive = false;
            }

        }
    }
    public void SetQuestComplete(Quest_DataHolder Quest)
    {
        Quest.SaveName = Quest.Id + Quest.QuestName;
        PlayerPrefs.SetInt(Quest.SaveName + "_IsCompleted_Data", 1);
        PlayerPrefs.SetInt(Quest.SaveName + "_IsActive_Data", 0);
        SetUpAllQuests();
    }

    public void SetQuestActive(Quest_DataHolder Quest)
    {
        Quest.SaveName = Quest.Id + Quest.QuestName;
        PlayerPrefs.SetInt(Quest.SaveName + "_IsActive_Data", 1);
        SetUpAllQuests();
    }

    public void ViewQuestUI(Quest_DataHolder Quest)
    {
        QuestUIHolder.SetActive(true);
        QuestName.text = Quest.QuestName;
        QuestDescription.text = Quest.QuestDescription;
        Gmanager.CanMove = false;
        //stop the controller ui;
    }
    public void ViewFirstQuestUi()
    {
        Quest_DataHolder q;
        Gmanager.CanMove = false;
        if (AllQuests[0] != null && AllQuests[0].IsActive)
        {
            ViewQuestUI(AllQuests[0]);

        }
        else
        {
            QuestName.text = " ";
            QuestDescription.text = "No Quest Yet...";
        }

    }
    public void StopQuestUI()
    {
        QuestUIHolder.SetActive(false);
        Gmanager.CanMove = true;
    }


}
