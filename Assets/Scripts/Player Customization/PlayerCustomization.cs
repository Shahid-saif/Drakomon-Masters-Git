using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomization : MonoBehaviour
{


    [Header("Charactor properties")]
    [SerializeField] bool IsSelectionMenu = true;
    public PlayerCustomizationClass playerCust;
    [SerializeField] GameObject MaleHead;
    [SerializeField] GameObject FemaleHead;

    [SerializeField] Material HairMat;

    [SerializeField] List<PartsClass> maleParts;
    [SerializeField] List<PartsClass> FemaleParts;
    //[SerializeField] List<GameObject> SelectionParts;

    [SerializeField] PlayerCustomizationClass Defult_maleParts;
    [SerializeField] PlayerCustomizationClass Defult_FemaleParts;
   public bool IsMale = true;



    [Space]
    [Header("Selection properties")]
    [SerializeField] List<GameObject> HairMale;
    [SerializeField] List<GameObject> HairFemale;

    [SerializeField] List<GameObject> BodyMale;
    [SerializeField] List<GameObject> BodyFemale;

    [SerializeField] List<GameObject> BagMale;
    [SerializeField] List<GameObject> BagFemale;
    [SerializeField] List<HairColorClass> HairColors;



    [Space]
    [Header("Save And load properties")]
    [SerializeField] PlayerData playerData;


    [System.Serializable]
    public class PartsClass
    {
       public string Id;
        public GameObject Part;
    }
    [System.Serializable]
    public class HairColorClass
    {
        public string Id;
        public Color HairColor;
    }
    void Start()
    {
        if (IsSelectionMenu)
        {
            SwitchGenderSelect(true);
            playerCust = Defult_maleParts;
            SetupCust(playerCust);
        }
        if (!IsSelectionMenu)
        {
            playerCust.HairId = playerData.HairId;
            playerCust.BodyId = playerData.BodyId;
            playerCust.HiarColor_id = playerData.HairColorId;
            playerCust.bagId = playerData.BagId;
            StopAllParts(maleParts);
            StopAllParts(FemaleParts);
            if (playerData.IsMale)
            {
                playerCust.Hiar = LookingForPart(maleParts, playerData.HairId);
                playerCust.Body = LookingForPart(maleParts, playerData.BodyId);
                playerCust.Bag = LookingForPart(maleParts, playerData.BagId);
                playerCust.HiarColor = LookingForHairColor(HairColors, playerData.HairColorId);
            }
            else
            {
                playerCust.Hiar = LookingForPart(FemaleParts, playerData.HairId);
                playerCust.Body = LookingForPart(FemaleParts, playerData.BodyId);
                playerCust.Bag = LookingForPart(FemaleParts, playerData.BagId);
                playerCust.HiarColor = LookingForHairColor(HairColors, playerData.HairColorId);  
            }
            SetupCust(playerCust);
            //  LoadSaveCustomization();
        }
 
    }
    void StopAllParts(List<GameObject> g)
    {
        foreach (var item in g)
        {
            item.SetActive(false);
        }
    }

    void StopAllParts(List<PartsClass> g)
    {
        foreach (var item in g)
        {
            item.Part.SetActive(false);
        }
    }
    public void SetupCust(PlayerCustomizationClass PCclass)
    {
        StopAllParts(maleParts);
        StopAllParts(FemaleParts);
        HairMat.color = PCclass.HiarColor;
        PCclass.Hiar.SetActive(true);
        PCclass.Body.SetActive(true);
        PCclass.Bag.SetActive(true);
        playerData.SaveBody(PCclass.BodyId);
        playerData.SaveHair(PCclass.HairId);
        playerData.SaveHairColor(PCclass.HiarColor_id);
        playerData.SaveBag(PCclass.bagId);
    }
    public void SwitchGender()
    {
        IsMale =! IsMale;
        if (IsMale)
        {
            StopAllParts(maleParts);
            StopAllParts(FemaleParts);
            SetupCust(Defult_maleParts);
            playerCust = Defult_maleParts;
            MaleHead.SetActive(true);
            FemaleHead.SetActive(false);
            playerData.SaveGender(0);

        }
        else
        {
            StopAllParts(maleParts);
            StopAllParts(FemaleParts);
            SetupCust(Defult_FemaleParts);
            playerCust = Defult_FemaleParts;
            MaleHead.SetActive(false);
            FemaleHead.SetActive(true);
            playerData.SaveGender(1);

        }
        playerData.LoadPlayerCustomization();
    }
    public void SwitchGenderSelect(bool _IsMale)
    {
        IsMale = _IsMale;
        if (_IsMale)
        {
            StopAllParts(maleParts);
            StopAllParts(FemaleParts);
            SetupCust(Defult_maleParts);
            playerCust = Defult_maleParts;
            MaleHead.SetActive(true);
            FemaleHead.SetActive(false);
            playerData.SaveGender(0);
        }
        else
        {
            StopAllParts(maleParts);
            StopAllParts(FemaleParts);
            SetupCust(Defult_FemaleParts);
            playerCust = Defult_FemaleParts;
            MaleHead.SetActive(false);
            FemaleHead.SetActive(true);
            playerData.SaveGender(1);
        }
    }
    public GameObject LookingForPart(List<PartsClass> g,string PartId)
    {
        foreach (var part in g)
        {
            if (part.Id==PartId)
            {
                return part.Part;
            }
         
        }
        return null;
    }
    public Color LookingForHairColor (List<HairColorClass> g, string ColorId)
    {
        foreach (var part in g)
        {
            if (part.Id == ColorId)
            {
                return part.HairColor;
            }

        }
        return Color.black;
    }
    public void FillPlayerCust_Hair(string Hair)
    {
        StopAllParts(HairMale);
        StopAllParts(HairFemale);
        if (IsMale)
        {
            playerCust.Hiar = LookingForPart(maleParts,Hair);
        }
        else
        {
            playerCust.Hiar = LookingForPart(FemaleParts, Hair);
        }
        playerData.SaveHair(Hair);
        playerCust.HairId = Hair;
        SetupCust(playerCust);
    }
    public void FillPlayerCust_Body(string Body)
    {
        StopAllParts(BodyMale);
        StopAllParts(BodyFemale);
        if (IsMale)
        {
            playerCust.Body = LookingForPart(maleParts, Body);
        }
        else
        {
            playerCust.Body = LookingForPart(FemaleParts, Body);
        }
        playerData.SaveBody(Body);
        playerCust.BodyId = Body;
    
        SetupCust(playerCust);
    }
    public void FillPlayerCust_Bag(string Bag)
    {
        StopAllParts(BodyMale);
        StopAllParts(BodyFemale);
        if (IsMale)
        {
            playerCust.Bag = LookingForPart(maleParts, Bag);
        }
        else
        {
            playerCust.Bag = LookingForPart(FemaleParts, Bag);
        }
        playerData.SaveBag(Bag);
        playerCust.bagId = Bag;
      
        SetupCust(playerCust);
    }
    public void FillPlayerCust_HairColor(string HairColor)
    {
        //StopAllParts(BagMale);
        //StopAllParts(BagFemale);
        playerCust.HiarColor = LookingForHairColor(HairColors,HairColor);
        playerData.SaveHairColor(HairColor);
        playerCust.HiarColor_id = HairColor;
        SetupCust(playerCust);
    }

    public void LoadSaveCustomization()
    {
            playerData.LoadPlayerCustomization();
            IsMale = playerData.IsMale;

        FillPlayerCust_Hair(playerData.HairId);
        FillPlayerCust_Body(playerData.BodyId);
        FillPlayerCust_Bag(playerData.BagId);
        FillPlayerCust_HairColor(playerData.HairColorId);
    }
}
