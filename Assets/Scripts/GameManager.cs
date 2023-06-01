using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{

    [Header("Global Ref")]
    public GameObject PlayerUiHolder;
    public GameObject PlayerControllerUiHolder;
    public Camera MainCam;
    public Transform Player;
    public Transform Player_follow;
    public bool CanMove=true;
    public bool CanUseSkate = true;
    public bool CanHavePet = true;
    public bool InDialog = false;

    [Space]
    [Header("Start setup")]
    [SerializeField] Transform StartPos;

    [Space]
    [Header("Player Info")]
    public PlayerDataHolder playerData;

    [Space]
    [Header("Global Events")]
    public UnityEvent OnUpdate;
    public UnityEvent OnStart;

 

    [Space]
    [Header("Intract")]
    public UnityEvent OnInteract;
    public GameObject InteractUiHolder;
    public Image InteractImg;

    [Space]
    [Header("Run time data")]
    public RunTimeData RunTime_playerSpownPos;

    #region Singleton
    public static GameManager Instance;
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (RunTime_playerSpownPos.BackFromRoom)
        {
            Player.transform.position = RunTime_playerSpownPos.position;
            RunTime_playerSpownPos.BackFromRoom = false;
        }
        else
        {
            if (StartPos!=null)
            Player.position = StartPos.position;
        }
        OnStart?.Invoke();
        InteractUiHolder.SetActive(false);
        PlayerUiHolder.SetActive(true);
        PlayerControllerUiHolder.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate?.Invoke();

    }
    public void SetPlayerSpownPosition(Transform pos)
    {
        RunTime_playerSpownPos.position = pos.position;
    }
    public void SetPlayerSpownIsFromRoom(bool isFRoom)
    {
        RunTime_playerSpownPos.BackFromRoom = isFRoom;
    }
}
