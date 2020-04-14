using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSetter : MonoBehaviour
{

    public SpriteRenderer texture;

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
        if(PlayerPrefs.GetInt("Skin") == 1)
        {
            texture.GetComponent<SpriteRenderer>().sprite = pizzaSprites[0];
        }
        
        if(PlayerPrefs.GetInt("Skin") == 2)
        {
            texture.GetComponent<SpriteRenderer>().sprite = pizzaSprites[1];
        }
        
        if(PlayerPrefs.GetInt("Skin") == 3)
        {
            texture.GetComponent<SpriteRenderer>().sprite = pizzaSprites[2];
        }
        
        if(PlayerPrefs.GetInt("Skin") == 4)
        {
            texture.GetComponent<SpriteRenderer>().sprite = pizzaSprites[3];
        }
    }
}
