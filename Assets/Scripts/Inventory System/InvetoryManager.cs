using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class InvetoryManager : MonoBehaviour
{

    [SerializeField] GameManager GManager;

    [Header("Items properties")]
    [SerializeField] List<Item> AllItems;


    [Space]
    [Header("UI properties")]
    [SerializeField] GameObject UsableButton;
    [SerializeField] GameObject UsableForMonster;
    [SerializeField] GameObject Inventory_sellMenu;
    [SerializeField] GameObject BuyingMenu;
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemDiscription;
    [SerializeField] TextMeshProUGUI PlayerMony;
    [SerializeField] GameObject MonsterEffectSelectMenu;

    [Space]
    [Header("Selling properties")]
    public Item TransactionItem;
    public int TransectionCount = 1;

    [Space]
    [Header("Shop properties")]
    public Shop shop;
    public GameObject ShopSellUI;
    public GameObject ShopBuyUI;
    [SerializeField] List<Item> AllShopItems;

    [Space]
    [Header("Sell UI properties")]
    public bool IsSelling = false;
    public bool IsBuying = false;
    public TextMeshProUGUI SellPriceText;
    public TextMeshProUGUI TractionCountText;
    public GameObject TransactoinObj;

    [Space]
    [Header("item Effect properties")]
    public MonsterItemEffect_UI MonsterEffectUi;

    [Space]
    [Header("UI Item Effect properties")]
    [SerializeField] Monster M;
    [SerializeField] float ItemEffectTime = 0.1f;
    public UnityEvent OnUseItem;
    #region singleton
    public static InvetoryManager Instance;
    #endregion
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GManager = GameManager.Instance;
        SetUpAllItems();
        //debuge
        PlayerPrefs.SetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
    }

    public void SetUpAllItems()
    {
        Inventory_sellMenu.SetActive(false);
        BuyingMenu.SetActive(false);

        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
        if (IsSelling || !IsBuying)
        {

            Inventory_sellMenu.SetActive(true);
            TransactoinObj.SetActive(true);
        }
        if (IsBuying)
        {
            SetUpShop_buy();
            BuyingMenu.SetActive(true);
            TransactoinObj.SetActive(true);
        }

        UsableButton.SetActive(false);
        UsableForMonster.SetActive(false);
        TransactoinObj.SetActive(false);
        MonsterEffectSelectMenu.SetActive(false);



        GManager.playerData.PlayerSaveMony = PlayerPrefs.GetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
        PlayerMony.text = "$" + GManager.playerData.PlayerSaveMony;
    }
    public void SetUpUsedAllItems()
    {
        Inventory_sellMenu.SetActive(false);
        BuyingMenu.SetActive(false);

        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
        if (IsSelling || !IsBuying)
        {

            Inventory_sellMenu.SetActive(true);
            TransactoinObj.SetActive(true);
        }
        if (IsBuying)
        {
            SetUpShop_buy();
            BuyingMenu.SetActive(true);
            TransactoinObj.SetActive(true);
        }

        UsableButton.SetActive(false);
        UsableForMonster.SetActive(false);
        TransactoinObj.SetActive(false);

        GManager.playerData.PlayerSaveMony = PlayerPrefs.GetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
        PlayerMony.text = "$" + GManager.playerData.PlayerSaveMony;
    }

    public void AddItemCount(Item item)
    {
        PlayerPrefs.SetInt(item.DataHolder.SaveDataName + "ItemCount", item.DataHolder.SaveItemCount + 1);
        item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
        SetUpAllItems();
    }
    public void GiveItemCount(Item item)
    {
        if (item.DataHolder.SaveItemCount > 0)
        {
            PlayerPrefs.SetInt(item.DataHolder.SaveDataName + "ItemCount", item.DataHolder.SaveItemCount - 1);
            item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
            SetUpAllItems();
        }
    }

    public void UseItemCount(Item item)
    {
        if (item.DataHolder.SaveItemCount > 0)
        {
            PlayerPrefs.SetInt(item.DataHolder.SaveDataName + "ItemCount", item.DataHolder.SaveItemCount - 1);
            item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);

            SetUpUsedAllItems();


        }
    }
    public void ShowSelectedInfo(Item item)
    {

        TransectionCount = 1;
        TransactionItem = item;
        UsableButton.SetActive(false);
        UsableForMonster.SetActive(false);
        TransactoinObj.SetActive(false);
        MonsterEffectSelectMenu.SetActive(false);
        if (IsSelling)
        {
            TransactoinObj.SetActive(true);
            SellPriceText.text = "Sell for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }
        else if (IsBuying)
        {
            TransactoinObj.SetActive(true);
            SellPriceText.text = "Buy for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }
        else
        {
            if (item.DataHolder.Usable)
            {
                UsableButton.SetActive(true);
            }
            if (item.DataHolder.UsableForMonsters)
            {
                UsableForMonster.SetActive(true);
            }
        }
        TransectionCount = 1;
        ItemName.text = item.DataHolder.ItemName;
        ItemDiscription.text = item.DataHolder.ItemDiscription;
    }

    public void ClearInfo()
    {
        UsableButton.SetActive(false);
        UsableForMonster.SetActive(false);
        MonsterEffectSelectMenu.SetActive(false);
        ItemName.text = "";
        ItemDiscription.text = "";
        SellPriceText.text = "";
        TransectionCount = 1;
        TractionCountText.text = TransectionCount.ToString();
        TransactionItem = null;
    }

    public void PostionFilter()
    {
        ClearInfo();
        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0 && item.DataHolder.IsPostion)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
    }
    public void KeyFilter()
    {
        ClearInfo();
        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0 && item.DataHolder.IsKey)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
    }
    public void EggFilter()
    {
        ClearInfo();
        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0 && item.DataHolder.IsEgg)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
    }
    public void ALlFilter()
    {
        ClearInfo();
        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
    }
    public void BookFilter()
    {
        foreach (var item in AllItems)
        {
            if (item.DataHolder.SaveItemCount > 0 && item.DataHolder.IsBook)
            {
                item.ItemSceneObj.SetActive(true);

                //Set Save data
                item.DataHolder.SaveDataName = item.DataHolder.ItemName + item.DataHolder.ID;
                item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
                item.DataHolder.ItemCount = item.DataHolder.SaveItemCount;
                item.ItemCount.text = "X" + item.DataHolder.ItemCount.ToString();


            }
            else
            {
                item.ItemSceneObj.SetActive(false);
            }
        }
    }

    #region Shop
    public void SetIsSelling(bool isselling)
    {
        IsSelling = isselling;
    }
    public void SetIsBuying(bool Isbuying)
    {
        IsBuying = Isbuying;
    }
    public void StopAllShopItems()
    {
        foreach (var item in AllShopItems)
        {
            item.ItemSceneObj.SetActive(false);
        }
    }
    public void SetCurrentShop(Shop _shop)
    {
        shop = _shop;
    }
    public void SetUpShop_buy()
    {
        StopAllShopItems();

        foreach (var item in shop.ShopItems)
        {

            item.ItemSceneObj.SetActive(true);

            item.ItemCount.text = "$" + item.DataHolder.Price.ToString();
        }
    }


    public void ResetTransactionCount()
    {
        TransectionCount = 1;
    }

    //public void AddTransactionCount_Buy()
    //{
    //    if (GManager.playerData.PlayerMony>TransactionItem.DataHolder.Price* TransectionCount)
    //    {
    //        TransectionCount += 1;
    //    }
    //    SellPriceText.text = "Buy for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
    //    TractionCountText.text = TransectionCount.ToString();
    //}
    public void AddTransactionCount()
    {
        if (IsBuying)
        {
            if (GManager.playerData.PlayerMony - TransactionItem.DataHolder.Price * TransectionCount >= TransactionItem.DataHolder.Price)
            {
                TransectionCount += 1;
            }
            SellPriceText.text = "Buy for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }
        else
        {
            if (TransactionItem.DataHolder.SaveItemCount > TransectionCount)
            {
                TransectionCount += 1;
            }
            SellPriceText.text = "Sell for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }

    }
    public void SubTransactionCount()
    {

        if (TransectionCount > 1)
        {
            TransectionCount -= 1;
        }
        if (TransectionCount < 1)
        {
            TransectionCount = 1;
        }
        if (IsBuying)
        {
            SellPriceText.text = "Buy for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }
        else
        {
            SellPriceText.text = "Sell for " + "$" + (TransactionItem.DataHolder.Price * TransectionCount).ToString();
            TractionCountText.text = TransectionCount.ToString();
        }



    }



    public void BuyItme()
    {
        for (int i = 0; i < TransectionCount; i++)
        {
            if (TransactionItem.DataHolder.SaveItemCount > 0)
            {
                PlayerPrefs.SetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", TransactionItem.DataHolder.SaveItemCount + 1);
                TransactionItem.DataHolder.SaveItemCount = PlayerPrefs.GetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", 0);
                GManager.playerData.PlayerMony -= TransactionItem.DataHolder.Price;
                PlayerPrefs.SetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
            }
        }
        SetUpAllItems();
        ClearInfo();
    }
    public void Execute_Transaction()
    {
        if (TransactionItem == null)
        {
            return;
        }
        if (IsBuying)
        {
            if (GManager.playerData.PlayerMony <= 0 || GManager.playerData.PlayerMony < TransactionItem.DataHolder.Price)
            {
                SellPriceText.text = "You don't have enough money";
                return;
            }

            for (int i = 0; i < TransectionCount; i++)
            {
                //if (TransactionItem.DataHolder.SaveItemCount > 0)
                {
                    PlayerPrefs.SetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", TransactionItem.DataHolder.SaveItemCount + 1);
                    TransactionItem.DataHolder.SaveItemCount = PlayerPrefs.GetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", 0);
                    GManager.playerData.PlayerMony -= TransactionItem.DataHolder.Price;
                    PlayerPrefs.SetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
                }
            }
        }
        else
        {
            for (int i = 0; i < TransectionCount; i++)
            {
                if (TransactionItem.DataHolder.SaveItemCount > 0)
                {
                    PlayerPrefs.SetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", TransactionItem.DataHolder.SaveItemCount - 1);
                    TransactionItem.DataHolder.SaveItemCount = PlayerPrefs.GetInt(TransactionItem.DataHolder.SaveDataName + "ItemCount", 0);
                    GManager.playerData.PlayerMony += TransactionItem.DataHolder.Price;
                    PlayerPrefs.SetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
                }
            }
        }
        GManager.soundManger.PlayeSoundEffectWithId("Transaction_eff");
        SetUpAllItems();
        ClearInfo();

    }
    #endregion

    #region ItemEffect

    public void EffectHp(int Effect, Monster M)
    {
        int EHp = Effect + M.DataHolder.saveHp;
        if (EHp > M.DataHolder.maxHP)
        {
            EHp = M.DataHolder.maxHP;
        }
        PlayerPrefs.SetInt(M.DataHolder.SaveName + "Hp", EHp);
    }

    public void EffectXp(int Effect, Monster M)
    {
        int EXp = Effect + M.DataHolder.saveXp;
        if (EXp >= M.DataHolder.MaxXP)
        {
            EXp -= M.DataHolder.MaxXP;
            int L = M.DataHolder.SaveLevel + 1;
            PlayerPrefs.SetInt(M.DataHolder.SaveName + "Level", L);
        }

        PlayerPrefs.SetInt(M.DataHolder.SaveName + "Xp", EXp);


    }
    MonsterItemEffect_UI MT;

    bool OnUse = false;
    public void MonsterValueEffect(Monster _M)
    {
        if (!OnUse)
        {
            M = _M;
            OnUse = true;
            GManager.soundManger.PlayeSoundEffectWithId("MosterItem_Use");
            if (MonsterUICoroutine != null)
            {
                if (MT != null)
                {
                    MT.EffectButton.interactable = true;
                    MT.XPslider.gameObject.SetActive(false);
                    MT.HPslider.gameObject.SetActive(false);
                    MT.Level.gameObject.SetActive(false);

                }

                StopCoroutine(MonsterUICoroutine);
            }

            // UpdateMonsterUI(M);

            MonsterEffectUi.XPslider.maxValue = _M.DataHolder.MaxXP;
            MonsterEffectUi.HPslider.maxValue = _M.DataHolder.maxHP;

            _M.DataHolder.saveHp = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Hp", 100);
            _M.DataHolder.saveXp = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Xp", 0);
            _M.DataHolder.SaveLevel = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Level", 1);

            _M.DataHolder.HP = _M.DataHolder.saveHp;
            _M.DataHolder.XP = _M.DataHolder.saveXp;
            _M.DataHolder.Level = _M.DataHolder.SaveLevel;

            MonsterEffectUi.XPslider.value = _M.DataHolder.XP;
            MonsterEffectUi.HPslider.value = _M.DataHolder.HP;

            SetMonsterUIActive(true);

            //if (TransactionItem.DataHolder.EffectXP <= 0 && M.DataHolder.HP == M.DataHolder.maxHP)
            //{
            //    M.DataHolder.HP = M.DataHolder.saveHp;
            //    M.DataHolder.XP = M.DataHolder.saveXp;
            //    M.DataHolder.Level = M.DataHolder.SaveLevel;
            //    MonsterUICoroutine = StartCoroutine(UpdateUi(M));
            //    return;
            //}

            EffectXp(TransactionItem.DataHolder.EffectXP, _M);
            EffectHp(TransactionItem.DataHolder.EffectHP, _M);

            _M.DataHolder.saveHp = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Hp", 100);
            _M.DataHolder.saveXp = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Xp", 0);
            _M.DataHolder.SaveLevel = PlayerPrefs.GetInt(_M.DataHolder.SaveName + "Level", 1);


            _M.DataHolder.HP = _M.DataHolder.saveHp;
            _M.DataHolder.XP = _M.DataHolder.saveXp;
            _M.DataHolder.Level = _M.DataHolder.SaveLevel;


            MonsterUICoroutine = StartCoroutine(UpdateUi());
            UseItemCount(TransactionItem);

            if (GManager.monsterManager.SelectedMonster == _M)
                GManager.monsterManager.SetupMonsterUI(GManager.monsterManager.SelectedMonster);

            if (GManager.fightSceneManager.IsFightScene)
            {
                this.gameObject.SetActive(false);
                GManager.fightSceneManager.byForceEnemyTurn();
            }
        }
    }

    public void UpdateMonsterUI()
    {

        //MonsterEffectUi.XPslider.maxValue = M.DataHolder.MaxXP;
        //MonsterEffectUi.HPslider.maxValue = M.DataHolder.maxHP;

        //MonsterEffectUi.XPslider.value = M.DataHolder.XP;
        //MonsterEffectUi.HPslider.value = M.DataHolder.HP;

        MonsterEffectUi.XPslider.value = Mathf.MoveTowards(MonsterEffectUi.XPslider.value, M.DataHolder.XP, ItemEffectTime * Time.deltaTime);
        MonsterEffectUi.HPslider.value = Mathf.MoveTowards(MonsterEffectUi.HPslider.value, M.DataHolder.HP, ItemEffectTime * Time.deltaTime);

        if (MonsterEffectUi.XPslider.value == M.DataHolder.XP && MonsterEffectUi.HPslider.value == M.DataHolder.HP)
        {
            MonsterEffectUi.Level.text = "LV." + M.DataHolder.Level.ToString();
            StartCoroutine(UpdateUiAfterEffect());
            //SetMonsterUIActive(false);
            //MT.EffectButton.interactable = true;
            //if (TransactionItem.DataHolder.SaveItemCount <= 0)
            //{
            //    SetUpAllItems();
            //}
            OnUseItem?.Invoke();
            OnUse = false;
            GManager.OnUpdate.RemoveListener(UpdateMonsterUI);
        }


    }

    public void SetMonsterUIActive(bool active)
    {
        if (active)
        {
            MonsterEffectUi.XPslider.gameObject.SetActive(true);
            MonsterEffectUi.HPslider.gameObject.SetActive(true);
            MonsterEffectUi.Level.gameObject.SetActive(true);
        }
        else
        {
            MonsterEffectUi.XPslider.gameObject.SetActive(false);
            MonsterEffectUi.HPslider.gameObject.SetActive(false);
            MonsterEffectUi.Level.gameObject.SetActive(false);
        }
    }
    Coroutine MonsterUICoroutine;
    IEnumerator UpdateUi()
    {

        MT = MonsterEffectUi;
        MT.EffectButton.interactable = false;
        yield return new WaitForSeconds(0.3f);
        GManager.OnUpdate.AddListener(UpdateMonsterUI);

        //SetMonsterUIActive(false);
        //MT.EffectButton.interactable = true;
        //if (TransactionItem.DataHolder.SaveItemCount <= 0)
        //{
        //    SetUpAllItems();
        //}

    }

    IEnumerator UpdateUiAfterEffect()
    {

        yield return new WaitForSeconds(0.3f);
        SetMonsterUIActive(false);
        MT.EffectButton.interactable = true;
        if (TransactionItem.DataHolder.SaveItemCount <= 0)
        {
            SetUpAllItems();
        }

    }
    #endregion

    #region On Fight Itmes

    public void EggUse_OnFight()
    {
        Debug.Log("Exe Egg use");
        if (GManager.fightSceneManager.IsFightScene)
        {
            //if (TransactionItem.DataHolder.IsEgg)
            //{
            GManager.fightSceneManager.EggEffect();
            UseItemCount(TransactionItem);
            this.gameObject.SetActive(false);
            //}
        }
    }
    #endregion
}
