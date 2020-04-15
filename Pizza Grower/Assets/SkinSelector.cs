using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [Header("Pizza/Box Content Box")]
    [SerializeField] private GameObject pizzaParent;
    [SerializeField] private GameObject boxParent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkinSetter(int skinNum)
    {
        if(skinNum == 1)
        {
            PlayerPrefs.SetInt("Skin", 1);
        }
        
        if(skinNum == 2)
        {
            PlayerPrefs.SetInt("Skin", 2);
        }
        
        if(skinNum == 3)
        {
            PlayerPrefs.SetInt("Skin", 3);
        }
        if(skinNum == 4)
        {
            PlayerPrefs.SetInt("Skin", 4);
        }
    }

    public void OpenPizzas()
    {
        boxParent.SetActive(false);
        pizzaParent.SetActive(true);
    }

    public void OpenBoxes()
    {
        pizzaParent.SetActive(false);
        boxParent.SetActive(true);
    }
}
