using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestViewer : MonoBehaviour
{

    public Quest_DataHolder Quest;
    public TextMeshProUGUI QuestButtonName;
    [SerializeField] QuestManager Qmanger;
    [Header("UIs")]
    public GameObject OnProgressImg;
    public GameObject OnCompleteImg;
    private void Start()
    {
        Qmanger = QuestManager.Instance;
    }
    public void ViewQuestUI()
    {
        Qmanger.ViewQuestUI(Quest);
    }
    }
