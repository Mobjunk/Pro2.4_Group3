using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Highscore : JsonHandler<HighscoreEntry>
 {
     [Header("Main menu variables")]
     [SerializeField] private GameObject highscorePrefab;
     [SerializeField] private GameObject highscoreParent;
     private Color defaultColor = new Color(0.6117647f, 0.4117647f, 0.1294118f);
     private Color secondColor = new Color(0.5372549f, 0.3647059f, 0.1254902f);
     
     /// <summary>
     /// Name of the json file
     /// </summary>
     /// <returns>The name of the json file its saving/loading</returns>
     protected override string GetFileName()
     {
         return "highscores.json";
     }
 
     /// <summary>
     /// The path of where the file can be found
     /// </summary>
     /// <returns>The path where the json file can be found</returns>
     protected override string GetPath()
     {
         return $"{Application.persistentDataPath}/SaveData";
     }

     public override void Start()
     {
         base.Start();
         
         //Handles sorting the list by days surviving (top -> bottom)
         //entries.Sort((a, b) => a.daysSurvived.CompareTo(b.daysSurvived));
         //entries.Reverse();
         
         //Checks if the script is being loaded in the main menu
         if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
             UpdateUI();
     }

     public void UpdateUI()
     {
         foreach (Transform child in highscoreParent.transform)
             Destroy(child.gameObject);
         
     }

    public void Add(HighscoreEntry entry)
    {
        entries.Add(entry);
        Save();
    }
}