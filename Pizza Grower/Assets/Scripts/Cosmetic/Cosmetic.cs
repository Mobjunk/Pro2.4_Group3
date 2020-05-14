using UnityEngine;

[CreateAssetMenu(fileName = "Cosmetic", menuName = "New Cosmetic")]
public class Cosmetic : ScriptableObject
{
    public int id;
    public Sprite sprite;
    public int cost;
}
