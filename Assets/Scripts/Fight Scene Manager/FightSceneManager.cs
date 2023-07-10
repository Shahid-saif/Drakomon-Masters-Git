using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class FightSceneManager : MonoBehaviour
{
    public bool IsFightScene;
    public bool IsOver = false;
    public bool PlayerWin;
    public GameManager Gmanager;
    public MonsterManager monsterManager;
    public static FightSceneManager Instance;

    [Header("Monsters properties")]
    public FightScene_FightDataHolder Scene_FightDataHolder;
    public int FightMonsterIndex;
    public List<FightEnemyMonster> AllFightMonsters;
    public List<FightEnemyMonster> FightMonsters;
    public FightEnemyMonster SelectedMonster;

    [Header("Fight Properties")]
    public Transform PlayerMonsterPos;
    public Transform EnemyMonsterPos;

    public Transform PlayerPos;
    public Transform EnemyPos;

    [SerializeField] float AttackMoveSpeed;

    [Space]
    [Header("Fight UI properties")]
    public GameObject FightMainMenu;
    [SerializeField] List<GameObject> StopUI;
    public GameObject MessageHolder;
    public TextMeshProUGUI MessageText;

    public GameObject FightMenu;
    public GameObject SelectMonsterMenu;

    public GameObject PlayerHealthHolder;
    public Slider PlayerHealthSlider;
    public TextMeshProUGUI PlayerMonsterName;
    public TextMeshProUGUI PlayerMonsterLevel;

    public GameObject EnemyHealthHolder;
    public Slider EnemyHealthSlider;
    public TextMeshProUGUI EnemyMonsterName;
    public TextMeshProUGUI EnemyMonsterLevel;

    [Space]
    public Transform SkillParentHolder;
    public List<GameObject> SkillUIObj;
    [Space]

    [Header("Fight UI Color properties")]
    public Color HealthNormalColor;
    public Color HealthDamagedColor;

    [Header("Effects Properties")]
    public ParticleSystem SwitchEffect;


    [Header("Attack Properties")]
    public bool PlayerTurn;
    public int SkillIndex;
    public FightAttackDataHolder Skill;
    public Animator Anim;
    public Animator HitAnim;
    public Transform Trans;
    public Transform TargetTrans;
    public Transform HomePoint;
    public Transform TargetHomePoint;
    public int AttackDamageAmount;
    public UnityEvent AttackFinishEvent;
    public string AttackerName;

    public MosterSkillUI Skill_UIs_Info;


    [Header("Attack Finish Properties")]
    public UnityEvent OnWinFight;
    public UnityEvent OnLoseFight;


    [Space]
    [Header("Fight Cam Properties")]
    public float CamMoveSpeed = 3f;
    public Transform TargetAngle;
    [SerializeField] Transform Cam;
    [SerializeField] Transform NormalCam;
    [SerializeField] Transform MoveToCam;
    [SerializeField] Transform PojectailCam;
    [SerializeField] Transform PlayerWinCam;
    [SerializeField] Transform PlayerLoseCam;



    [Space]
    [Header("Monster selection fight ui Properties")]
    public List<Button> SelectMonstersButtons;

    private void Awake()
    {
        Instance = this;
    }
    public void SetUpStart()
    {
        Gmanager = GameManager.Instance;
        if (IsFightScene)
        {
            FightMainMenu.SetActive(true
                );
            Gmanager.CanMove = false;
            Gmanager.CanUseSkate = false;
            Gmanager.CanHavePet = false;
            SetUpFigthScene();

            if(Gmanager.monsterManager.SelectedMonster!=null)
            UpdateHealthUI(Gmanager.monsterManager.SelectedMonster.DataHolder.HP, Gmanager.monsterManager.SelectedMonster.DataHolder.maxHP, PlayerHealthSlider);

            UpdateHealthUI(SelectedMonster.Health, SelectedMonster.MaxHealth, EnemyHealthSlider);
            UpdateAllHealthUI();
            SetSelectedPlayerMonsterUI();
            if (Scene_FightDataHolder.EnemyHuman != null)
            {
                GameObject O = Instantiate(Scene_FightDataHolder.EnemyHuman, EnemyPos.position, EnemyPos.rotation);
            }
            Gmanager.soundManger.PlayeSoundEffectWithId("StartBattle");
        }


    }

    public void SetUpFigthScene()
    {
        SetupFightMonster();
        Gmanager.monsterManager.OnSelectMonster.AddListener(SetSelectedPlayerMonsterUI);
        Gmanager.monsterManager.OnSelectMonster.AddListener(UpdateAllHealthUI);
        foreach (var UI in StopUI)
        {
            UI.gameObject.SetActive(false);
        }
       // FightMenu.SetActive(true);
        SwitchEffect.Play();
        Gmanager.monsterManager.SwitchEffect.Play();

        Gmanager.Player.position = PlayerPos.position;
        Gmanager.Player.rotation = PlayerPos.rotation;

        SelectedMonster.transform.position = EnemyMonsterPos.transform.position;
        SelectedMonster.transform.rotation = EnemyMonsterPos.transform.rotation;

        if (Gmanager.monsterManager.SelectedMonster!=null)
        {
            Gmanager.monsterManager.SelectedMonster.transform.position = PlayerMonsterPos.position;
            Gmanager.monsterManager.SelectedMonster.transform.rotation = PlayerMonsterPos.rotation;
        }


        EnemyHealthSlider.maxValue = SelectedMonster.MaxHealth;



    }

    public void SetupFightMonster()
    {
        foreach (var MonsterId in Scene_FightDataHolder.EnemyMonstersIDs)
        {
            foreach (var Monster in AllFightMonsters)
            {
                if (Monster.Id == MonsterId)
                {
                    FightMonsters.Add(Monster);
                }
            }
        }

        SelectMonsterToFight();

        //Next Fight Monster
        // FightMonsterIndex++;
    }

    void ClearAllMonsters()
    {
        foreach (var Monster in FightMonsters)
        {
            Monster.gameObject.SetActive(false);
        }
    }
    public void SelectMonsterToFight()
    {
        SwitchEffect.Play();

        if (FightMonsterIndex < FightMonsters.Count)
        {
            ClearAllMonsters();
            if (FightMonsters[FightMonsterIndex] != null)
            {
                SelectedMonster = FightMonsters[FightMonsterIndex];
                SelectedMonster.gameObject.SetActive(true);
                SelectedMonster.transform.position = EnemyMonsterPos.transform.position;
                SelectedMonster.transform.rotation = EnemyMonsterPos.transform.rotation;
            }
            else
            {
                CheckLastEnemyMoster();
            }


        }
        else
        {
            CheckLastEnemyMoster();
        }
        EnemyHealthSlider.maxValue = SelectedMonster.MaxHealth;
    }
    public void SwitchMonster()
    {

    }

    public void UpdateUI()
    {

    }

    public void AssingPlayerToAttack(int AttackIndex)
    {
        if (IsOver)
        {
            return;
        }
        AttackFinishEvent.RemoveAllListeners();
        AttackFinishEvent.AddListener(PlayerAttackDamage);
        PlayerHealthHolder.SetActive(false);
        EnemyHealthHolder.SetActive(true);
        Skill = Gmanager.monsterManager.SelectedMonster.FightDataHolder[AttackIndex];
        Anim = Gmanager.monsterManager.SelectedMonster.Anim;
        HitAnim = SelectedMonster.Anim;
        Trans = Gmanager.monsterManager.SelectedMonster.transform;
        TargetTrans = SelectedMonster.transform;
        HomePoint = PlayerMonsterPos;
        TargetHomePoint = EnemyMonsterPos;
        AttackDamageAmount = Gmanager.monsterManager.SelectedMonster.FightDataHolder[AttackIndex].AttackPower - SelectedMonster.DataHolder.Defense;
        if (AttackDamageAmount < 0)
        {
            AttackDamageAmount = 0;
        }
        AttackerName = Gmanager.monsterManager.SelectedMonster.DataHolder.Name;
        SkillName = Gmanager.monsterManager.SelectedMonster.FightDataHolder[AttackIndex].AttackID;
        SetAttack();
        SetCameraAngleValues(Skill);
        PlayerTurn = false;
    }
    public void collectSkillInfo(MosterSkillUI UIs)
    {
        Skill_UIs_Info = UIs;
    }
    public void AssingPlayerToAttackSkillUI()
    {
        if (IsFightScene)
        {
          
            AssingPlayerToAttack(Skill_UIs_Info.SkillIndex);
        }

    }
    public void SetSelectedPlayerMonsterUI()
    {

        ClearAllSelectedMonsterSkillUI();

        foreach (var skill in Gmanager.monsterManager.SkillItemList)
        {

            SkillIndex = Gmanager.monsterManager.SkillItemList.IndexOf(skill);
            Debug.Log(SkillIndex + "" + Gmanager.monsterManager.SkillItemList.IndexOf(skill));
            GameObject O = Instantiate(skill, SkillParentHolder);
            SkillUIObj.Add(O);
            //Button b = O.GetComponent<Button>();
            //if (b != null)
            //{
            //        b.onClick.AddListener(AssingPlayerToAttackSkillUI);

            //}

        }

    }
    string SkillName;
    public void AssingEnemyToAttack(int AttackIndex)
    {
        if (IsOver)
        {
            return;
        }
        AttackFinishEvent.RemoveAllListeners();
        AttackFinishEvent.AddListener(EnemyAttackDamage);
        EnemyHealthHolder.SetActive(false);
        PlayerHealthHolder.SetActive(true);
        Skill = SelectedMonster.FightDataHolder[AttackIndex];
        Anim = SelectedMonster.Anim;
        HitAnim = Gmanager.monsterManager.SelectedMonster.Anim;
        Trans = SelectedMonster.transform;
        TargetTrans = Gmanager.monsterManager.SelectedMonster.transform;
        HomePoint = EnemyMonsterPos;
        TargetHomePoint = PlayerMonsterPos;
        AttackDamageAmount = SelectedMonster.FightDataHolder[AttackIndex].AttackPower - Gmanager.monsterManager.SelectedMonster.DataHolder.Defense;
        if (AttackDamageAmount < 0)
        {
            AttackDamageAmount = 0;
        }
        SkillName = SelectedMonster.FightDataHolder[AttackIndex].AttackID;
        AttackerName = SelectedMonster.Id;
        //  SetCameraAngleValues(Skill);
        SetAttack();
    }
    public void SetAttack()
    {
        MessageText.text = AttackerName + " Execute " + SkillName;
        MessageHolder.SetActive(true);
        FightMenu.SetActive(false);
        SelectMonsterMenu.SetActive(false);
        //yield return new WaitForSeconds(1f);
        //MessageHolder.SetActive(false);
        Gmanager.OnUpdate.AddListener(Attack);
    }
    public void UpdateAllHealthUI()
    {
        if (Gmanager.monsterManager.SelectedMonster!=null)
        {
            UpdateHealthUI(Gmanager.monsterManager.SelectedMonster.DataHolder.HP, Gmanager.monsterManager.SelectedMonster.DataHolder.saveHp, PlayerHealthSlider);
            PlayerMonsterName.text = Gmanager.monsterManager.SelectedMonster.DataHolder.Name;
            PlayerMonsterLevel.text = "LV." + Gmanager.monsterManager.SelectedMonster.DataHolder.Level.ToString();
        }

        if (SelectedMonster!=null)
        {
            UpdateHealthUI(SelectedMonster.Health, SelectedMonster.MaxHealth, EnemyHealthSlider);
            EnemyMonsterName.text = SelectedMonster.Id;
            EnemyMonsterLevel.text = "LV." + SelectedMonster.Level.ToString();
        }

    }
    public void UpdateHealthUI(int Health, int MaxHealth, Slider HealthSLider)
    {
        if (Health < MaxHealth / 3)
        {
            HealthSLider.fillRect.GetComponent<Image>().color = HealthDamagedColor;
        }
        else
        {
            HealthSLider.fillRect.GetComponent<Image>().color = HealthNormalColor;
        }
        HealthSLider.value = Health;
    }
    public void PlayerAttackDamage()
    {
        //AttackDamageAmount = SelectedMonster.Health- SelectedMonster.DataHolder.Defense;
        //if (AttackDamageAmount<0)
        //{
        //    AttackDamageAmount = 0;
        //}
        SelectedMonster.Health -= AttackDamageAmount;
        UpdateHealthUI(SelectedMonster.Health, SelectedMonster.MaxHealth, EnemyHealthSlider);
        if (SelectedMonster.Health <= 0)
        {
            FightMonsterIndex++;
            SelectMonsterToFight();
            UpdateAllHealthUI();
            AttackFinishEvent.RemoveListener(PlayerAttackDamage);
        }
    }

    public void EnemyAttackDamage()
    {
        if (Gmanager.monsterManager.SelectedMonster==null)
        {
            return;
        }
        else
        {
            if (Gmanager.monsterManager.ActiveMonster.Count <= 1)
            {
                if (AttackDamageAmount >= Gmanager.monsterManager.SelectedMonster.DataHolder.saveHp)
                {
                    IsOver = true;
                    PlayerPrefs.SetInt(Gmanager.monsterManager.SelectedMonster.DataHolder.SaveName + "Hp", 5);
                    UpdateHealthUI(Gmanager.monsterManager.SelectedMonster.DataHolder.HP, Gmanager.monsterManager.SelectedMonster.DataHolder.maxHP, PlayerHealthSlider);
                    CheckLastPlayerMonster();
                }
                else
                {
                    //AttackDamageAmount = AttackDamageAmount - Gmanager.monsterManager.SelectedMonster.DataHolder.Defense;
                    //if (AttackDamageAmount<0)
                    //{
                    //    AttackDamageAmount = 0;
                    //}
                    Gmanager.monsterManager.DamageSelectedMonster(AttackDamageAmount);
                    UpdateHealthUI(Gmanager.monsterManager.SelectedMonster.DataHolder.HP, Gmanager.monsterManager.SelectedMonster.DataHolder.maxHP, PlayerHealthSlider);

                }
            }
            else
            {
                //AttackDamageAmount = AttackDamageAmount - Gmanager.monsterManager.SelectedMonster.DataHolder.Defense;
                //if (AttackDamageAmount < 0)
                //{
                //    AttackDamageAmount = 0;
                //}
                Gmanager.monsterManager.DamageSelectedMonster(AttackDamageAmount);
                UpdateHealthUI(Gmanager.monsterManager.SelectedMonster.DataHolder.HP, Gmanager.monsterManager.SelectedMonster.DataHolder.maxHP, PlayerHealthSlider);
            }
        }
       
    }
    public void Attack()
    {

        if (Skill.MoveToEnemy)
        {
            if (Vector3.Distance(Trans.position, TargetHomePoint.position) > 2f)
            {
                Trans.position = Vector3.MoveTowards(Trans.position, TargetHomePoint.position, AttackMoveSpeed * Time.deltaTime);
                Anim.SetFloat("Move", 1);
            }
            else
            {
                Anim.SetFloat("Move", 0);
                GameObject go = Instantiate(Skill.effect, TargetTrans.position, TargetTrans.rotation);
                Anim.CrossFade(Skill.Animation, 0.1f);
                HitAnim.Play("Hit");
                Gmanager.soundManger.PlayeSoundEffectWithId(Skill.AttackSound);
                ParticleSystem ps = go.GetComponent<ParticleSystem>();
                if (ps != null)
                {
                    ps.Play();
                }

                StartCoroutine(RetunToPositionWait());
                Gmanager.OnUpdate.RemoveListener(Attack);
            }
        }
        else
        {
            Anim.SetFloat("Move", 0);
            GameObject go = Instantiate(Skill.effect, Trans.position, Trans.rotation);
            Anim.CrossFade(Skill.Animation, 0.1f);
            HitAnim.Play("Hit");
            Gmanager.soundManger.PlayeSoundEffectWithId(Skill.AttackSound);
            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
            StartCoroutine(RetunToPositionWait());
            Gmanager.OnUpdate.RemoveListener(Attack);
        }
     
    }

    public void RetunToPosition()
    {
        if (Trans == null|| HomePoint==null)
        {
            Gmanager.OnUpdate.RemoveListener(RetunToPosition);
        }
        else
        {
            if (Vector3.Distance(Trans.position, HomePoint.position) > 0f)
            {
                Trans.position = Vector3.MoveTowards(Trans.position, HomePoint.position, (AttackMoveSpeed * 2) * Time.deltaTime);

            }
            else
            {
                Trans.position = HomePoint.position;
          

                if (!PlayerTurn && !IsOver)
                    StartCoroutine(EnemySetAttack());
                if (!IsOver)
                {
                    StartCoroutine(ShowUI());
                }

                TargetAngle = NormalCam;
                AddCameraAngleToUpdate();
                Gmanager.soundManger.PlayeSoundEffectWithId("RetunToPosition");
                Gmanager.OnUpdate.RemoveListener(RetunToPosition);
            }

        }


    }

    public void ClearAllSelectedMonsterSkillUI()
    {
        foreach (var skill in SkillUIObj)
        {
            Destroy(skill.gameObject);
        }
        SkillUIObj.Clear();
    }

   

    IEnumerator RetunToPositionWait()
    {
        MessageText.text = AttackerName + " Make Damage by " + AttackDamageAmount;
        yield return new WaitForSeconds(Skill.timeAfterAttack);
        AttackFinishEvent?.Invoke();
        Gmanager.OnUpdate.AddListener(RetunToPosition);

    }

    IEnumerator ShowUI()
    {
        MessageHolder.SetActive(false);
        yield return new WaitForSeconds(3f);
        EnemyHealthHolder.SetActive(true);
        PlayerHealthHolder.SetActive(true);

        yield return new WaitForSeconds(1f);
        if (PlayerTurn&&Gmanager.monsterManager.SelectedMonster!=null&&!IsOver)
        {
            FightMenu.SetActive(true);
        }

        // MessageHolder.SetActive(true);

    }

    IEnumerator EnemySetAttack()
    {
        yield return new WaitForSeconds(1f);
        int SkillIndex = Random.Range(0, SelectedMonster.FightDataHolder.Length);
        AssingEnemyToAttack(SkillIndex);
        PlayerTurn = true;
    }

    #region Camera

    public void SetCameraAngle(Transform _TargetAngle, float _CamMoveSpeed)
    {
        if (_TargetAngle != null)
        {
            Cam.transform.position = Vector3.MoveTowards(Cam.position, _TargetAngle.position, _CamMoveSpeed * Time.deltaTime);
            Cam.rotation = Quaternion.Lerp(Cam.rotation, _TargetAngle.rotation, _CamMoveSpeed / 2 * Time.deltaTime);
        }

    }

    public void SetCameraToAngle()
    {
        SetCameraAngle(TargetAngle, CamMoveSpeed);
        if (Vector3.Distance(Cam.position, TargetAngle.position) <= 0)
        {
            Cam.position = TargetAngle.position;
            Cam.rotation = TargetAngle.rotation;

            Gmanager.OnUpdate.RemoveListener(SetCameraToAngle);
        }
    }

    public void AddCameraAngleToUpdate()
    {
        Gmanager.OnUpdate.AddListener(SetCameraToAngle);
    }
    public void SetCameraAngleValues(FightAttackDataHolder skill)
    {
        if (skill.IsProjectile)
        {
            TargetAngle = PojectailCam;
        }
        if (skill.MoveToEnemy)
        {
            TargetAngle = MoveToCam;
        }
        AddCameraAngleToUpdate();
    }

    #endregion

    #region Finish Fight
    public void CheckLastEnemyMoster()
    {
        IsOver = true;
        PlayerWin = true;
        SetFightUsed(true);
        SelectedMonster.Anim.Play("Die");
        Gmanager.monsterManager.SelectedMonster.Anim.Play("Win");
        // StopAllCoroutines();
        StartCoroutine(GoToScene());
    }
    public void CheckLastPlayerMonster()
    {
        IsOver = true;
        PlayerWin = false;
        SetFightUsed(true);
        SelectedMonster.Anim.Play("Win");
        Gmanager.monsterManager.SelectedMonster.Anim.Play("Die");
        // StopAllCoroutines();
        StartCoroutine(GoToScene());
    }
    IEnumerator GoToScene()
    {
        MessageHolder.SetActive(false);
        EnemyHealthHolder.SetActive(false);
        PlayerHealthHolder.SetActive(false);
        FightMenu.SetActive(false);
        SelectMonsterMenu.SetActive(false);
        yield return new WaitForSeconds(3f);
        TargetAngle = PlayerLoseCam;
        AddCameraAngleToUpdate();
        yield return new WaitForSeconds(4f);
        TargetAngle = PlayerWinCam;
        AddCameraAngleToUpdate();
        Gmanager.soundManger.setMusicPlay(false);
        if (PlayerWin)
        {
            Gmanager.soundManger.PlayeSoundEffectWithId("Fight Win");
            MessageText.text = "You Win";
        }
        else
        {
            Gmanager.soundManger.PlayeSoundEffectWithId("Fight Lose");
            MessageText.text = "You lost";
        }
        MessageHolder.SetActive(true);
        yield return new WaitForSeconds(3f);
        AdsInitializer.instance.interstitialAdExample.ShowAd();
  
            Gmanager.sceneManager.GoToSceneByName(Scene_FightDataHolder.ReturnSceneName);

      
    }
    #endregion

    #region StartFight
    public void StartFight(FightScene_FightDataHolder _FightDataHolder)
    {

        if (_FightDataHolder.Used)
        { return; }
        Scene_FightDataHolder.Prev = _FightDataHolder;
        if (_FightDataHolder.EnemyHuman != null)
        {
            Scene_FightDataHolder.EnemyHuman = _FightDataHolder.EnemyHuman;
        }

        Scene_FightDataHolder.FightRounds = _FightDataHolder.FightRounds;
        Scene_FightDataHolder.FightSceneName = _FightDataHolder.FightSceneName;
        Scene_FightDataHolder.ReturnSceneName = _FightDataHolder.ReturnSceneName;
        Scene_FightDataHolder.CanExit = _FightDataHolder.CanExit;
        Scene_FightDataHolder.EnemyMonstersIDs.Clear();

        foreach (var MonsterId in _FightDataHolder.EnemyMonstersIDs)
        {
            Scene_FightDataHolder.EnemyMonstersIDs.Add(MonsterId);
        }
        Gmanager.SetPlayerSpownPosition(Gmanager.Player);
        Gmanager.sceneManager.GoToSceneByName(Scene_FightDataHolder.FightSceneName);
    }
    public void SetFightUsed(bool Used)
    {
        Scene_FightDataHolder.Prev.Used = Used;
    }
    #endregion

    #region Item Effect Monsters
    [SerializeField] ParticleSystem TunMosterToAlies_eff;
    [SerializeField] ParticleSystem FailMosterToAlies_eff;
    public void byForceEnemyTurn()
    {
        if (IsFightScene)
        {
            PlayerTurn = false;
            SelectMonsterMenu.SetActive(false);
            MessageHolder.SetActive(false);
            FightMenu.SetActive(false);

            //  Gmanager.invetoryManager.gameObject.SetActive(false);
            StartCoroutine(EnemySetAttack());
        }
    }

    public void EggEffect()
    {
        Debug.Log("Exe EggEffect");
        if (Gmanager.invetoryManager.TransactionItem.DataHolder.EggEffect > SelectedMonster.DataHolder.EggRes)
        {
            Debug.Log("egg effect is big");

                PlayerPrefs.SetInt(SelectedMonster.DataHolder.SaveName + "IsActive", 1);
               // Gmanager.monsterManager.SetUpAllMonsters();
                TunMosterToAlies_eff.transform.position = SelectedMonster.transform.position;
                TunMosterToAlies_eff.Play();
                FightMonsterIndex++;
                SelectMonsterToFight();
                //CheckLastEnemyMoster();
                byForceEnemyTurn();
    
        }
        else
        {
            Debug.Log("egg effect is small");
            FailMosterToAlies_eff.transform.position = SelectedMonster.transform.position;
            FailMosterToAlies_eff.Play();
            byForceEnemyTurn();
        }
    }

    #endregion

    #region SelectMonsterUI
    public void StopButtonInteract(Button b)
    {
        AddButtonToMonsterButtons(b);
        foreach (var button in SelectMonstersButtons)
        {
            button.interactable = true;
        }
        b.interactable = false;
     
    }
    public void AddButtonToMonsterButtons(Button b)
    {
        foreach (var button in SelectMonstersButtons)
        {
            if (b==button)
            {
                return;
            }
        }
        SelectMonstersButtons.Add(b);
    }
    #endregion

}
