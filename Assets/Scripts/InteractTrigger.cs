using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractTrigger : MonoBehaviour
{
    [SerializeField] UnityEvent IntractEvetn;
    [SerializeField] Sprite InteractImg;
    GameManager Gmanager;
    void Start()
    {
        Gmanager = GameManager.Instance;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gmanager.InteractUiHolder.SetActive(true);
            Gmanager.InteractImg.sprite = InteractImg;
            Gmanager.OnInteract.AddListener(ExeInter);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gmanager.InteractUiHolder.SetActive(false);
            Gmanager.OnInteract.RemoveListener(ExeInter);
        }
    }

    public void ExeInter()
    {
        IntractEvetn?.Invoke();
    }
}
