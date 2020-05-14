using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSetter : MonoBehaviour
{
    public static SkinSetter instance;
    public Image texture;

    public List<Sprite> pizzaSprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SkinChecker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkinChecker()
    {
        texture.sprite = CosmeticHandler.instance.cosmeticDefinition[PlayerPrefs.GetInt("Skin")].sprite;
    }
}
