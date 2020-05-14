using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticHandler : MonoBehaviour
{

    public static CosmeticHandler instance;

    [SerializeField] GameObject cosmeticPrefab;
    [SerializeField] GameObject grid;
    [SerializeField] Text feedback;
    private float feedbackTimer;
    public GameObject buyMenu;
    public List<Cosmetic> cosmeticDefinition;
    public bool[] unlocked;

    private void Awake()
    {
        PlayerPrefs.SetInt($"unlocked:0", 1);
        instance = this;

        cosmeticDefinition.Sort((a, b) => a.id.CompareTo(b.id));

        unlocked = new bool[cosmeticDefinition.Count];
        for (int index = 0; index < unlocked.Length; index++)
            unlocked[index] = PlayerPrefs.GetInt($"unlocked:{index}") == 1;
    }

    private void Start()
    {
        for (int index = 0; index < cosmeticDefinition.Count; index++)
        {
            Cosmetic cosmectic = cosmeticDefinition[index];
            GameObject cosmeticObject = Instantiate(cosmeticPrefab, grid.transform, true);

            cosmeticObject.transform.localScale = new Vector3(1, 1, 1);

            cosmeticObject.name = $"Pizza:{index}";

            Image image = cosmeticObject.GetComponent<Image>();
            image.sprite = cosmectic.sprite;

            cosmeticObject.GetComponent<CosmeticData>().pizzaIndex = index;
        }
    }

    private void Update()
    {
        if(feedbackTimer > 0)
        {
            feedbackTimer -= Time.deltaTime;
            if(feedbackTimer <= 0)
            {
                feedback.text = "";
                feedbackTimer = 0;
            }
        }
    }

    public void Close()
    {
        buyMenu.SetActive(false);
    }

    public void SwitchSkin(int pizzaIndex)
    {
        PlayerPrefs.SetInt("Skin", pizzaIndex);
        SetFeedback("You have switched the skin!");
    }

    public void PurchaseSkin()
    {
        int index = int.Parse(buyMenu.name.Replace("Buy menu", ""));
        Cosmetic cosmetic = cosmeticDefinition[index];
        int currentCoins = PlayerPrefs.GetInt("Coins");

        buyMenu.name = "Buy menu";
        buyMenu.SetActive(false);

        //Check if the player has enough coins
        if (currentCoins < cosmetic.cost)
        {
            Debug.Log("Not enough coins");
            SetFeedback("Not enough coins to purchase the skin.");
            return;
        }

        SetFeedback("Succesfully purchased the skin!");
        PlayerPrefs.SetInt("Coins", currentCoins - cosmetic.cost);
        unlocked[index] = true;
        PlayerPrefs.SetInt($"unlocked:{index}", 1);
        SwitchSkin(index);
    }

    void SetFeedback(string message)
    {
        feedback.text = message;
        feedbackTimer = 4;
    }
}
