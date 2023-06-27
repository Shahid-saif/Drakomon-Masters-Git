using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterItemEffect_UI : MonoBehaviour
{
    public Slider HPslider;
    public Slider XPslider;
    public TextMeshProUGUI Level;
    public Button EffectButton;

    [SerializeField] GameManager Gmanager;
    private void Start()
    {
        Gmanager = GameManager.Instance;
        HPslider.gameObject.SetActive(false);
        XPslider.gameObject.SetActive(false);
        Level.gameObject.SetActive(false);
    }

    public void SetMonsterEffectUI()
    {
        Gmanager.invetoryManager.MonsterEffectUi = this;
    }
}
