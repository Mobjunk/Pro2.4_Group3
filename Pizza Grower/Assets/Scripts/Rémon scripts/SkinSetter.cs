using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSetter : MonoBehaviour
{

    public Image texture;

    public List<Sprite> pizzaSprites = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        SkinChecker();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkinChecker()
    {
        texture.sprite = pizzaSprites[PlayerPrefs.GetInt("Skin") - 1];
    }
}
