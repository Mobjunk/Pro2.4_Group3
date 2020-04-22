using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetection : MonoBehaviour
{
    //public BoxCollider2D pizzaBox;
    //public BoxCollider2D[] pizzaBoxColliders;

    //public CircleCollider2D pizzaCollider;
    //public CircleCollider2D holeCollider;

    public GameObject box;
    public GameObject Pizza;


    //public Transform maxSize;

    public Vector3 minSize;
    public Vector3 maxSize;

    public PizzaGrower growthScript;
    //scale van pizza moet gechekt worden zodra touch wordt losgelaten
    //groter dan gat in doos
    //kleiner dan doos

    public void Start()
    {
        //box.GetComponentInChildren<BoxCollider2D>();

        minSize = GameObject.Find("BoxHole").transform.localScale;
        maxSize = box.transform.localScale;

        //holeCheck();
    }

     

    public void holeCheck()
    {
        if (growthScript.Click == false)
        {
            Debug.Log("pizza has been grown/clicked");

            if (Pizza.transform.localScale.magnitude > minSize.magnitude)
            {
                Debug.Log("pizza has passed the hole");
            }

            if (Pizza.transform.localScale.magnitude < maxSize.magnitude)
            {
                Debug.Log("It fits in the box");
            }

        }
      



    }

   

    /* private void OnTriggerEnter2D(Collider2D collision)
     {

         if (collision == hole && hole.transform.localScale.magnitude < pizza.transform.localScale.magnitude)
         {
             Debug.Log("nice");
         }

         if (collision == box && pizza.transform.localScale.magnitude < box.transform.localScale.magnitude  )
         {
             Debug.Log("Touched the box");
         }
         //if (collision == pizzaBox)
         //     Debug.Log("Touched the box");
         //}
     */
    //}


}
