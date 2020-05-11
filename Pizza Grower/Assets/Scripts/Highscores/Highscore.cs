using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Highscore : JsonHandler<HighscoreEntry>
{
    public static Highscore instance;

    [Header("Main menu variables")]
    [SerializeField] private GameObject highscorePrefab;
    [SerializeField] private GameObject highscoreParent;
    [SerializeField] private GameObject grid;
    [Header("Highscore entries loaded")]
    [SerializeField] private bool hasLoaded = false;
    [Header("Sorting the list")]
    [SerializeField] private string sortingBy = "Default";

    /// <summary>
    /// Name of the json file
    /// </summary>
    /// <returns>The name of the json file its saving/loading</returns>
    protected override string GetFileName()
    {
        return "";
    }

    /// <summary>
    /// The path of where the file can be found
    /// </summary>
    /// <returns>The path where the json file can be found</returns>
    protected override string GetPath()
    {
        return "";
    }

    /// <summary>
    /// The link where the json file is located
    /// </summary>
    /// <returns>The link where the json file is located</returns>
    protected override string GetLink()
    {
        return "https://mobstar-sof.com/school/export_highscore.php";
    }

    /// <summary>
    /// The link it uses to save/insert to
    /// </summary>
    /// <returns>The link it uses to save/insert to</returns>
    protected override string GetInsertLink()
    {
        return "http://mobstar-sof.com/school/insert_highscore.php";
    }

    public override void Start()
    {
        instance = this;
        LoadContent(false);
        this.hasLoaded = true;
    }

    public override void Update()
    {
        if (finishedLoading)
        {

            //Handles sorting the list by score (top -> bottom)
            entries.Sort((a, b) => a.score.CompareTo(b.score));
            entries.Reverse();

            SetupUI();
            finishedLoading = false;
        }
    }

    /// <summary>
    /// Handles inserting a entry to the database online
    /// </summary>
    /// <param name="entry">Handles inserting a entry to the database online</param>
    public override void Insert(HighscoreEntry entry)
    {
        StartCoroutine(InsertEntry(entry));
    }

    /// <summary>
    /// Handles loading the highscores
    /// </summary>
    /// <param name="clear">Should it clear the entries list</param>
    public void LoadContent(bool clear = false)
    {
        Debug.Log("Loaded highscores...");

        //Checks if it should clear the list before filling it
        if (clear) entries.Clear();

        //Loads the json file from a website
        Load(false);
    }

    public void SetupUI()
    {
        foreach (Transform child in grid.transform)
            Destroy(child.gameObject);


        foreach (var entry in getSortedList())
        {
            //Spawns the prefab inside the grid
            var entryObject = Instantiate(highscorePrefab, grid.transform, true);
            //Corrects the scale
            entryObject.transform.localScale = new Vector3(1, 1, 1);
            //Sets the name
            entryObject.transform.name = $"Highscore: {entry.id}";

            //Grabs the name, score and time played child
            var playerName = entryObject.transform.GetChild(0);
            var score = entryObject.transform.GetChild(1);
            var time_played = entryObject.transform.GetChild(2);

            //Sets the name, score and time played
            playerName.GetComponent<Text>().text = $"{entry.name}";
            score.GetComponent<Text>().text = $"{entry.score}";
            time_played.GetComponent<Text>().text = $"{entry.timed_played}";
        }
    }

    /// <summary>
    /// Handles opening the highscores panel
    /// </summary>
    public void Open()
    {
        highscoreParent.SetActive(true);

        if (this.hasLoaded) LoadContent(true);
    }

    /// <summary>
    /// Handles closing the highscores panel 
    /// </summary>
    public void Close()
    {
        highscoreParent.SetActive(false);
    }

    public void SwitchSort(string sort)
    {
        //Checks if the player already has this sort
        if (sortingBy.Equals(sort)) return;

        sortingBy = sort;
        SetupUI();
    }

    /// <summary>
    /// Handles sending a new entry to the highscores database
    /// </summary>
    /// <param name="entry">The highscore entry</param>
    /// <returns></returns>
    private IEnumerator InsertEntry(HighscoreEntry entry)
    {
        //Creates a new form
        WWWForm form = new WWWForm();

        //Fills the form with the required data
        form.AddField("name", entry.name);
        form.AddField("score", entry.score.ToString());
        form.AddField("time_played", entry.timed_played.ToString());
        form.AddField("gamemode", entry.gamemode);

         //Handles sending the web quest
        using (UnityWebRequest www = UnityWebRequest.Post(GetInsertLink(), form))
        {
            //Sends the web request
            yield return www.SendWebRequest();

            //Checks if there is no error
            if (!www.isNetworkError && !www.isHttpError)
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    /// <summary>
    /// A list based on what the sorting is set to
    /// </summary>
    /// <returns>A list with only the entries that have the same gamemode</returns>
    public List<HighscoreEntry> getSortedList()
    {
        return entries.Where(entry => entry.gamemode.Equals(sortingBy)).ToList();
    }
}