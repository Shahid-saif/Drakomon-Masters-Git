using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    [Header("Ref")]
    public MonsterManager monsterManager;
    public FightSceneManager fightSceneManager;
    public InvetoryManager invetoryManager;
    public QuestManager questManager;
    public DialogManager dialogManager;
    public Scene_Manager sceneManager;
    public SoundManager soundManger;

    [Header("Global Ref")]
    public GameObject PlayerUiHolder;
    public GameObject PlayerControllerUiHolder;
    public Camera MainCam;
    public Transform Player;
    public Transform Player_follow;
    public bool CanMove = true;
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

    [Space]
    [Header("Room setup")]
    [SerializeField]List< GameObject> RoomStopObj;
    #region Singleton
    public static GameManager Instance;
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        if (monsterManager == null)
            monsterManager = MonsterManager.Instance;
        if (fightSceneManager == null)
            fightSceneManager = FightSceneManager.Instance;
        if (invetoryManager == null)
            invetoryManager = InvetoryManager.Instance;
        if (questManager == null)
            questManager = QuestManager.Instance;
        if (dialogManager == null)
            dialogManager = DialogManager.Instance;
        if (sceneManager == null)
            sceneManager = Scene_Manager.Instance;
        if (soundManger==null)
            soundManger = SoundManager.Instance;

        if (RunTime_playerSpownPos.BackFromRoom)
        {
            Player.transform.position = RunTime_playerSpownPos.position;
            RunTime_playerSpownPos.BackFromRoom = false;
        }
        else
        {
            if (StartPos != null)
                Player.position = StartPos.position;
            Player.rotation = StartPos.rotation;
        }
        OnStart?.Invoke();
        InteractUiHolder.SetActive(false);
        PlayerUiHolder.SetActive(true);
        PlayerControllerUiHolder.SetActive(true);

        monsterManager.SetUpAllMonsters();
        if (CanHavePet)
        {
            monsterManager.FirstMonsterSelection();
          
        }
        else
        {
           // monsterManager.SelectedMonster.gameObject.SetActive(false);
            foreach (var roomObj in RoomStopObj)
            {
                roomObj.SetActive(false);
            }
        }


        fightSceneManager.SetUpStart();

      //  AdsInitializer.instance.interstitialAdExample.ShowAd();
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

    #region Ads_Connection
    public bool CheckForConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            return false;
        }
        return true;
    }
    #endregion
}
