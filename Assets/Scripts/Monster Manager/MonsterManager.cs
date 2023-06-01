using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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


            Monster.MonsterObj_patrolling.SetActive(false);
            if (Monster.MonsterObj_Fight != null)
                Monster.MonsterObj_Fight.SetActive(false);
            Monster.IsPatrolling = false;
            Monster.IsSelected = false;
            if (Monster.DataHolder.IsActive)
            {
                ActiveMonster.Add(Monster);
            }
        }
    }
    #endregion
    #region MainFunctions
    public void FirstMonsterSelection()
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


            Monster.MonsterObj_patrolling.SetActive(false);
            if (Monster.MonsterObj_Fight != null)
                Monster.MonsterObj_Fight.SetActive(false);
            Monster.IsPatrolling = false;
            Monster.IsSelected = false;
            if (Monster.DataHolder.IsActive)
            {
                ActiveMonster.Add(Monster);
            }
        }
    }
    public void SelectMonsterById(string Id)
    {

        foreach (var Monster in ActiveMonster)
        {
            if (Id == Monster.DataHolder.ID)
            {

                SelectedMonster = Monster;
                PlayerPrefs.SetString("SelectedMonster", Monster.DataHolder.ID);
                SetMonsterActive(Monster);
                SetUpActiveMonster_Patrolling(Monster);
                Monster.IsSelected = true;
                SetupMonsterUI(Monster);
                break;

            }
        }

    }

    public void SetUpActiveMonster_Patrolling(Monster M)
    {
        M.IsPatrolling = true;
        M.MonsterObj_patrolling.SetActive(true);
        M.MonsterObj_patrolling.transform.position = GManager.Player_follow.position;
    }
    public void SetMonsterActive(Monster M)
    {
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "IsActive", 1);
        SetUpAllMonsters();
    }
    public void SetMonsterDesActive(Monster M)
    {
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "IsActive", 0);
        SetUpAllMonsters();
    }

    public void SetUpActiveMonster_Fight(Monster M)
    { }
    #endregion

    #region UI
    public void SetupMonsterUI(Monster monster)
    {
        StopAllMonsterSceneUI();
        MonsterSceneUI MonsterUI = SelectMonsterSceneUI(monster.DataHolder.ID);
        if (MonsterUI!=null)
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
        MonsterAttack.text = monster.DataHolder.Attack.ToString();
        MonsterDefense.text = monster.DataHolder.Defense.ToString();
        MonsterSpeed.text = monster.DataHolder.Speed.ToString();
        MonsterLevel.text = "Lev." + monster.DataHolder.Level.ToString();

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
            Monster.Level.text= "Lev." + Monster.DataHolder.Level.ToString();
            if (Monster.DataHolder.IsActive)
            {
                Monster.MonsterSelectionButton.SetActive(true);
            }
        }
        

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
