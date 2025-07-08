using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data/Food set")]
public class EnemyStats : ScriptableObject
{
    public GameObject[] entries;

    //simply assigns the entries in the inspector
    public GameObject GetRandomEntry()
    {
        if (entries == null || entries.Length == 0)
        {
            return null;
        }
        int rand = Random.Range(0, entries.Length);
        return entries[rand];
    }
}