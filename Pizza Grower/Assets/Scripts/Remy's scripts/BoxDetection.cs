using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    

    public GameObject box;
    public GameObject pizza;
    public GameObject hole;

   

    public Vector3 minSize;
    public Vector3 maxSize;
    public Vector3 perfect;

    public PizzaGrower growthScript;

    public void Awake()
    {
        pizza.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void Start()
    {
        
        
        minSize = hole.transform.localScale;
        maxSize = box.transform.localScale;

        
    }

     

    public void holeCheck()
    {
        if (growthScript.Click == false)
        {
            Debug.Log("pizza has been grown/clicked");

            if (pizza.transform.localScale.magnitude > minSize.magnitude)
            {
                Debug.Log("pizza has passed the hole");
            }

            if (pizza.transform.localScale.magnitude < maxSize.magnitude)
            {
                Debug.Log("It fits in the box");
                //if (perfect >= maxSize - 0.25 && perfect <= maxSize;)
                //{
                    //x >= maxSize - 0.25 && x <= maxSize
                //}
            }

            if (pizza.transform.localScale.magnitude > maxSize.magnitude)
            {
                Debug.Log("The pizza was 2 big");
                GameObject.Destroy(pizza);
            }

        }
    
    }

   

  

}
