using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InvetoryManager : MonoBehaviour
{

    [SerializeField] GameManager GManager;

    [Header("Items properties")]
    [SerializeField] List<Item> AllItems;


    [Space]
    [Header("UI properties")]
    [SerializeField] GameObject UsableMenu;
    [SerializeField] GameObject UsableForMonster;
    [SerializeField] GameObject Inventory_sellMenu;
    [SerializeField] GameObject BuyingMenu;
    [SerializeField] TextMeshProUGUI ItemName;
    [SerializeField] TextMeshProUGUI ItemDiscription;
    [SerializeField] TextMeshProUGUI PlayerMony;

    [Space]
    [Header("Selling properties")]
    public Item TransactionItem;
    public int TransectionCount=1;

    [Space]
    [Header("Shop properties")]
    public Shop shop;
    public GameObject ShopSellUI;
    public GameObject ShopBuyUI;
    [SerializeField] List<Item> AllShopItems;

    [Space]
    [Header("Sell UI properties")]
    public bool IsSelling=false;
    public bool IsBuying = false;
    public TextMeshProUGUI SellPriceText;
    public TextMeshProUGUI TractionCountText;
    public GameObject TransactoinObj;


    void Start()
    {
        GManager = GameManager.Instance;
        SetUpAllItems();
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

        UsableMenu.SetActive(false);
        UsableForMonster.SetActive(false);
        TransactoinObj.SetActive(false);
      
    
        GManager.playerData.PlayerSaveMony = PlayerPrefs.GetInt(GManager.playerData.PlayerName + "Mony", GManager.playerData.PlayerMony);
        PlayerMony.text ="$" + GManager.playerData.PlayerSaveMony;
    }

    public void AddItemCount(Item item)
    {
       PlayerPrefs.SetInt(item.DataHolder.SaveDataName + "ItemCount", item.DataHolder.SaveItemCount +1);
        item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
        SetUpAllItems();
    }
    public void GiveItemCount(Item item)
    {
        if (item.DataHolder.SaveItemCount>0)
        {
            PlayerPrefs.SetInt(item.DataHolder.SaveDataName + "ItemCount", item.DataHolder.SaveItemCount - 1);
            item.DataHolder.SaveItemCount = PlayerPrefs.GetInt(item.DataHolder.SaveDataName + "ItemCount", 0);
            SetUpAllItems();
        }
    }

    public void ShowSelectedInfo(Item item)
    {
        TransectionCount = 1;
        TransactionItem = item;
        UsableMenu.SetActive(false);
        UsableForMonster.SetActive(false);
        TransactoinObj.SetActive(false);
    
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
                UsableMenu.SetActive(true);
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
        UsableMenu.SetActive(false);
        UsableForMonster.SetActive(false);

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
            if (item.DataHolder.SaveItemCount > 0&&item.DataHolder.IsPostion)
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
            if (item.DataHolder.SaveItemCount > 0 )
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
    } public void BookFilter()
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
            if (GManager.playerData.PlayerMony - TransactionItem.DataHolder.Price * TransectionCount>= TransactionItem.DataHolder.Price)
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
        if (TransactionItem==null)
        {
            return;
        }
        if (IsBuying)
        {
            if (GManager.playerData.PlayerMony  <= 0|| GManager.playerData.PlayerMony<TransactionItem.DataHolder.Price)
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
       
        SetUpAllItems();
        ClearInfo();

    }
    #endregion
}
