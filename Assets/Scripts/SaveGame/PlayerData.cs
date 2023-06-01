using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    [Header("Player Customization Data")]
    public bool IsMale;
    public string BodyId;
    public string HairId;
    public string HairColorId;
    public string BagId;


    #region Singleton
    public static PlayerData Instance;
    #endregion

    private void Awake()
    {
        Instance = this;
        LoadPlayerCustomization();
        if (PlayerPrefs.GetInt("Player_Gender", 0) > 0)
        {
            IsMale = false;
        }
        else
        {
            IsMale = true;
        }
    }
    void Start()
    {
       
    }

 

    public void SavePlayerCustomization(string Body,string hair,string hairColor, string bag,int IsMale)
    {
       PlayerPrefs.SetInt("Player_Gender", IsMale);
         PlayerPrefs.SetString("Player_Body", Body);
        PlayerPrefs.SetString("Player_Hair", hair);
        PlayerPrefs.SetString("Player_HairColor", hairColor);
         PlayerPrefs.SetString("Player_Bag", bag);
    }

    public void LoadPlayerCustomization()
    {

        BodyId = PlayerPrefs.GetString("Player_Body", "Body_male"); 
        HairId= PlayerPrefs.GetString("Player_Hair", "Hair_male");
        HairColorId= PlayerPrefs.GetString("Player_HairColor", "H_black");
        BagId= PlayerPrefs.GetString("Player_Bag", "Bag_male");
        if (PlayerPrefs.GetInt("Player_Gender", 0)>0)
        {
            IsMale = false;
        }
        else
        {
            IsMale = true;
        }

    }

    public void SaveBody(string Body)
    {
        PlayerPrefs.SetString("Player_Body", Body);
        LoadPlayerCustomization();
        Debug.Log("PlayerBody saved :" + Body);
    }

    public void SaveHair(string Hair)
    {
        PlayerPrefs.SetString("Player_Hair", Hair);
        LoadPlayerCustomization();
    }
    public void SaveHairColor(string hairColor)
    {
        PlayerPrefs.SetString("Player_HairColor", hairColor);
        LoadPlayerCustomization();
    }

    public void SaveBag(string bag)
    {
        PlayerPrefs.SetString("Player_Bag", bag);
        LoadPlayerCustomization();
    }

    public void SaveGender(int IsMale)
    {
        PlayerPrefs.SetInt("Player_Gender", IsMale);
        LoadPlayerCustomization();
    }


}
