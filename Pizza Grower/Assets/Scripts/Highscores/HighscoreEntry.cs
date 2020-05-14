[System.Serializable]
public class HighscoreEntry
{
    /// <summary>
    /// The database id that represents this highscore entry
    /// </summary>
    public int id;
    /// <summary>
    /// The name of the player
    /// </summary>
    public string name;
    /// <summary>
    /// The score the player got
    /// </summary>
    public int score;
    /// <summary>
    /// The amount of time the game lasted
    /// </summary>
    public int time;
    /// <summary>
    /// The game mode the player played
    /// </summary>
    public string gamemode;

    public HighscoreEntry(string name, int score, int time, string gamemode)
    {
        this.name = name;
        this.score = score;
        this.time = time;
        this.gamemode = gamemode;
    }
}
