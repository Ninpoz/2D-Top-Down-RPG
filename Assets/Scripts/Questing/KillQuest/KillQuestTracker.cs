using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KillQuestTRacker
{
    public int totalEnemies;
    public int killedEnemies = 0;
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;

    // Metod f�r att uppdatera antalet d�dade tr�d
    public void UpdateKilledTrees()
    {
        killedEnemies++;
        Debug.Log("Enemies Destroyed: " + killedEnemies + "/" + totalEnemies);
    }
}
