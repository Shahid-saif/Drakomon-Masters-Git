using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class MonsterManager : MonoBehaviour
{
    [Header("Monsters properties")]
    public List<Monster> AllMonsters;
    public List<Monster> ActiveMonster;
    public Monster SelectedMonster;

    [SerializeField] GameManager GManager;


    [Space]
    [Header("UI properties")]
    public List<MonsterSceneUI> MonsterSceneUIs;
    public TextMeshProUGUI MonsterName;
    public TextMeshProUGUI MonsterInfoName;
    public TextMeshProUGUI MonsterHp;
    public Slider MonsterHpSlider;
    public Slider MonsterXpSlider;
    public TextMeshProUGUI MonsterAttack;
    public TextMeshProUGUI MonsterDefense;
    public TextMeshProUGUI MonsterSpeed;
    public TextMeshProUGUI MonsterLevel;
    public TextMeshProUGUI MonsterType;
    public Image Type;

    [SerializeField] Sprite Fire;
    [SerializeField] Sprite Green;
    [SerializeField] Sprite Water;



    [Header("UI Skills properties")]
    public Transform SkillItemParent;
    public GameObject SkillItem_Info;
    public GameObject SkillItem_Fight;
    public List<GameObject> SkillItemList;

    [Header("Effects properties")]
    public ParticleSystem SwitchEffect;

    [Header("Events properties")]
    public UnityEvent OnSelectMonster;


    #region singleton
    public static MonsterManager Instance;
    #endregion

    [System.Serializable]
    public class MonsterSceneUI
    {
        public string Id;
        public GameObject Monster3dUI;
        public GameObject MonsterSelectionButton;
        public MonsterDataHolder DataHolder;

        public TextMeshProUGUI Name;
        public TextMeshProUGUI Level;

    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GManager = GameManager.Instance;
        SetUpAllMonsters();
        FirstMonsterSelection();
    }

    #region Debug
    public void ClearSaveData()
    {
        ActiveMonster.Clear();
        foreach (var Monster in AllMonsters)
        {
            //Load save data
            Monster.DataHolder.SaveName = Monster.DataHolder.Name + Monster.DataHolder.ID;

            PlayerPrefs.SetInt(Monster.DataHolder.SaveName + "Hp", 100);
            PlayerPrefs.SetInt(Monster.DataHolder.SaveName + "Xp", 0);
            PlayerPrefs.SetInt(Monster.DataHolder.SaveName + "Level", 1);
            PlayerPrefs.SetInt(Monster.DataHolder.SaveName + "IsActive", 0);


            Monster.DataHolder.HP = Monster.DataHolder.saveHp;
            Monster.DataHolder.XP = Monster.DataHolder.saveXp;
            Monster.DataHolder.Level = Monster.DataHolder.SaveLevel;
            if (Monster.DataHolder.SaveIsActive > 0)
            {
                Monster.DataHolder.IsActive = true;
            }
            else
            {
                Monster.DataHolder.IsActive = false;
            }

            Monster.MonsterObj.SetActive(false);
            //if (Monster.MonsterObj_Fight != null)
            //    Monster.MonsterObj_Fight.SetActive(false);
            //  Monster.IsPatrolling = false;
            Monster.IsSelected = false;
            if (Monster.DataHolder.IsActive)
            {
                ActiveMonster.Add(Monster);
            }
        }
    }

    public void TestSetMonsterActive(int active)
    {
        SetMonsterActive(SelectedMonster, active);
    }
    #endregion
    #region MainFunctions
    public void FirstMonsterSelection()
    {
        if (GManager.fightSceneManager.IsFightScene)
        {
            SelectedMonster = null;
            GManager.fightSceneManager.SelectMonsterMenu.SetActive(true);
            GManager.fightSceneManager.FightMenu.SetActive(false);
        }
        else
        {
            if (SelectedMonster == null)
            {
                SelectMonsterById(PlayerPrefs.GetString("SelectedMonster", "1"));
            }
            else
            {
                SelectMonsterById(SelectedMonster.DataHolder.ID);
            }
        }

    }
    public void SetUpAllMonsters()
    {
        ActiveMonster.Clear();

        foreach (var Monster in AllMonsters)
        {
            Monster.DataHolder.SaveName = Monster.DataHolder.Name + Monster.DataHolder.ID;
            //Load save data
            Monster.DataHolder.saveHp = PlayerPrefs.GetInt(Monster.DataHolder.SaveName + "Hp", 100);
            Monster.DataHolder.saveXp = PlayerPrefs.GetInt(Monster.DataHolder.SaveName + "Xp", 0);
            Monster.DataHolder.SaveLevel = PlayerPrefs.GetInt(Monster.DataHolder.SaveName + "Level", 1);
            Monster.DataHolder.SaveIsActive = PlayerPrefs.GetInt(Monster.DataHolder.SaveName + "IsActive", 0);


            Monster.DataHolder.HP = Monster.DataHolder.saveHp;
            Monster.DataHolder.XP = Monster.DataHolder.saveXp;
            Monster.DataHolder.Level = Monster.DataHolder.SaveLevel;
            if (Monster.DataHolder.SaveIsActive > 0)
            {
                Monster.DataHolder.IsActive = true;
            }
            else
            {
                Monster.DataHolder.IsActive = false;
            }


            Monster.MonsterObj.SetActive(false);

            //if (Monster.MonsterObj_Fight != null)
            //    Monster.MonsterObj_Fight.SetActive(false);
            //  Monster.IsPatrolling = false;
            Monster.IsSelected = false;
            if (Monster.DataHolder.IsActive)
            {
                ActiveMonster.Add(Monster);
                foreach (var UiEl in Monster.UiMonsterSelect)
                {
                    UiEl.SetActive(true);
                }

            }
            else
            {
                foreach (var UiEl in Monster.UiMonsterSelect)
                {
                    UiEl.SetActive(false);
                }
            }

            foreach (var UiSkills in Monster.UiMonsterSkills)
            {
                UiSkills.SetActive(false);
            }
        }
    }
    public void SelectMonsterById(string Id)
    {
        //if (SelectedMonster != null)
        //{
        //    if (Id == SelectedMonster.DataHolder.ID)
        //    {
        //        Debug.Log("Select the sam monster");
        //        return;
        //    }
        //}

        foreach (var Monster in ActiveMonster)
        {
            if (Id == Monster.DataHolder.ID)
            {

                SelectedMonster = Monster;
                PlayerPrefs.SetString("SelectedMonster", Monster.DataHolder.ID);
                SetMonsterActive(Monster);
                SetUpActiveMonster_IntoScene(Monster);
                Monster.IsSelected = true;
                SetupMonsterUI(Monster);
                OnSelectMonster?.Invoke();
                if (GManager.fightSceneManager.IsFightScene)
                {
                    GManager.fightSceneManager.byForceEnemyTurn();
                }
                //  Fmanager.SelectedMonster = Monster;
                break;

            }
        }

    }

    public void SetUpActiveMonster_IntoScene(Monster M)
    {


        if (GManager.fightSceneManager.IsFightScene)
        {
            //    M.IsPatrolling = false;
            // M.MonsterObj_Fight.SetActive(true);

            M.agent.enabled = false;
            M.Controller.enabled = false;
            M.MonsterObj.SetActive(true);
            M.MonsterObj.transform.position = GManager.fightSceneManager.PlayerMonsterPos.position;
            M.MonsterObj.transform.rotation = GManager.fightSceneManager.PlayerMonsterPos.rotation;
            M.MonsterObj.transform.localScale = M.FightScale;
        }
        else
        {
            //     M.IsPatrolling = true;
            //     M.MonsterObj_Fight.SetActive(false);

            M.agent.enabled = true;
            M.Controller.enabled = true;
            M.MonsterObj.SetActive(true);
            M.MonsterObj.transform.position = GManager.Player_follow.position;
            M.MonsterObj.transform.localScale = M.NormalScale;
        }
        SwitchEffect.Play();
        SwitchEffect.transform.position = M.transform.position;
    }
    public void SetMonsterActive(Monster M)
    {
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "IsActive", 1);
        SetUpAllMonsters();
    }
    public void SetMonsterDesActive(Monster M)
    {
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "IsActive", 0);
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "Hp", M.DataHolder.maxHP);
        SetUpAllMonsters();
    }

    //public void SetUpActiveMonster_Fight(Monster M)
    //{
    //    M.IsPatrolling = false;
    //    M.MonsterObj_patrolling.SetActive(false);
    //    M.MonsterObj_Fight.SetActive(true);
    //    M.MonsterObj_Fight.transform.position = Fmanager.PlayerMonsterPos.position;
    //}
    #endregion

    #region MonsterValues
    public void SetMonsterActive(Monster M, int active)
    {
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "IsActive", active);
        SetUpAllMonsters();
        if (SelectedMonster == M && active <= 0)
        {
            SelectedMonster = null;
            GManager.fightSceneManager.SwitchEffect.transform.position = GManager.fightSceneManager.PlayerMonsterPos.position;
            GManager.fightSceneManager.SwitchEffect.Play();
            //SelectMonsterById(ActiveMonster[0].DataHolder.ID);
            GManager.fightSceneManager.FightMenu.SetActive(false);
            if (!GManager.fightSceneManager.IsOver)
            {
                GManager.fightSceneManager.MessageHolder.SetActive(false);
            }
            GManager.fightSceneManager.SelectMonsterMenu.SetActive(true);
        }
        else
        {
            SelectMonsterById(SelectedMonster.DataHolder.ID);
        }

    }
    public void DamageSelectedMonster(int Damage)
    {
        if (SelectedMonster==null)
        {
            return;
        }
        else
        {
            int h = PlayerPrefs.GetInt(SelectedMonster.DataHolder.SaveName + "Hp", 100);
            h -= Damage;
            if (h < 0)
            {
                h = 0;
                //  PlayerPrefs.SetInt(SelectedMonster.DataHolder.SaveName + "Hp", h);
                PlayerPrefs.SetInt(SelectedMonster.DataHolder.SaveName + "Hp", SelectedMonster.DataHolder.maxHP);
                SetMonsterActive(SelectedMonster, 0);
            }
            else
            {
                PlayerPrefs.SetInt(SelectedMonster.DataHolder.SaveName + "Hp", h);
            }

            SelectedMonster.DataHolder.saveHp = PlayerPrefs.GetInt(SelectedMonster.DataHolder.SaveName + "Hp", 100);
            SelectedMonster.DataHolder.HP = SelectedMonster.DataHolder.saveHp;
            SetupMonsterUI(SelectedMonster);

        }


    }
    #endregion

    #region UI
    public void SetupMonsterUI(Monster monster)
    {
        StopAllMonsterSceneUI();
        MonsterSceneUI MonsterUI = SelectMonsterSceneUI(monster.DataHolder.ID);
        if (MonsterUI != null)
        {
            MonsterUI.Monster3dUI.SetActive(true);
            MonsterUI.MonsterSelectionButton.SetActive(true);
        }

        MonsterName.text = monster.DataHolder.Name;
        MonsterInfoName.text = monster.DataHolder.Name;
        MonsterHp.text = monster.DataHolder.HP.ToString();
        MonsterHpSlider.maxValue = monster.DataHolder.maxHP;
        MonsterHpSlider.value = monster.DataHolder.HP;
        MonsterXpSlider.maxValue = monster.DataHolder.MaxXP;
        MonsterXpSlider.value = monster.DataHolder.XP;
        MonsterAttack.text = monster.DataHolder.EggRes.ToString();
        MonsterDefense.text = monster.DataHolder.Defense.ToString();
        MonsterSpeed.text = monster.DataHolder.Speed.ToString();
        MonsterLevel.text = "Lev." + monster.DataHolder.Level.ToString();

        for (int i = 0; i < monster.FightDataHolder.Length; i++)
        {
            GameObject Go = Instantiate(SkillItem_Info, SkillItemParent);
            //Go.transform.position = SkillItem_Info.transform.position;
            //Go.transform.rotation = SkillItem_Info.transform.rotation;
            SkillItemList.Add(Go);
            Go.SetActive(true);
            MosterSkillUI mUI = Go.GetComponent<MosterSkillUI>();
            if (mUI != null)
            {
                mUI.SkillName.text = monster.FightDataHolder[i].AttackID;
                mUI.SkillImg.sprite = monster.FightDataHolder[i].AttackImg;
                mUI.Level.text = "LV." + monster.FightDataHolder[i].ActiveLevel.ToString();
                mUI.SkillIndex = i;
                Button B = mUI.GetComponent<Button>();
                if (B != null)
                {
                    if (monster.FightDataHolder[i].ActiveLevel < monster.DataHolder.Level)
                    {
                        B.interactable = true;
                    }
                    else
                    {
                        B.interactable = false;
                    }
                }
            }
        }

        if (monster.DataHolder.IsFire)
        {
            Type.sprite = Fire;
            MonsterType.text = "Fire";
        }

        if (monster.DataHolder.IsGreen)
        {
            Type.sprite = Green;
            MonsterType.text = "Green";
        }

        if (monster.DataHolder.IsWater)
        {
            Type.sprite = Water;
            MonsterType.text = "Water";
        }

        foreach (var UiSkills in monster.UiMonsterSkills)
        {
            UiSkills.SetActive(true);
        }
    }
    public void UpdateFightUi()
    {

    }
    public void UpdateInventoryUI()
    {

    }
    public MonsterSceneUI SelectMonsterSceneUI(string Id)
    {
        foreach (var Monster in MonsterSceneUIs)
        {
            if (Monster.Id == Id)
            {
                return Monster;
            }
        }
        return null;
    }
    public void StopAllMonsterSceneUI()
    {
        foreach (var Monster in MonsterSceneUIs)
        {
            Monster.Monster3dUI.SetActive(false);
            Monster.MonsterSelectionButton.SetActive(false);
            Monster.Name.text = Monster.DataHolder.Name;
            Monster.Level.text = "Lev." + Monster.DataHolder.Level.ToString();
            if (Monster.DataHolder.IsActive)
            {
                Monster.MonsterSelectionButton.SetActive(true);
            }


        }
        foreach (var Skill in SkillItemList)
        {
            Destroy(Skill);
        }
        SkillItemList.Clear();



    }
    public void SetUpMonsterSceneUI()
    {
        foreach (var Monster in MonsterSceneUIs)
        {
            if (Monster.DataHolder.IsActive)
            {
                Monster.MonsterSelectionButton.SetActive(true);
            }
        }
    }

    #endregion


}
