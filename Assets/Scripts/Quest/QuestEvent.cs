using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestEvent : MonoBehaviour
{
    [SerializeField] Quest_DataHolder Quest;

    [SerializeField] UnityEvent OnQuestComplete;
    [SerializeField] UnityEvent OnQuestActive;
    [SerializeField] UnityEvent OnQuestDesActive;
    [SerializeField] bool OnStart=false;

    private void Start()
    {
        if (OnStart)
        {
            StartCoroutine(SetEventOnStart());
        }
    }
    IEnumerator SetEventOnStart()
    {
        yield return new WaitForSeconds(0.2f);
        SetEvent();
    }
    public void SetEvent()
    {
        if (Quest.IsActive&&!Quest.IsCompleted)
        {
            OnQuestActive?.Invoke();
        }
        if (Quest.IsCompleted)
        {
            OnQuestComplete?.Invoke();
        }
        if (!Quest.IsCompleted&&!Quest.IsActive)
        {
            OnQuestDesActive?.Invoke();
        }
    }

}
