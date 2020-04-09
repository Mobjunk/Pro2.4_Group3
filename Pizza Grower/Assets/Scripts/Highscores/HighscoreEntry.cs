[System.Serializable]
public class HighscoreEntry
{
    /// <summary>
    /// The name of the player
    /// </summary>
    public string playerName;

    public HighscoreEntry(string playerName)
    {
        this.playerName = playerName;
    }
}
