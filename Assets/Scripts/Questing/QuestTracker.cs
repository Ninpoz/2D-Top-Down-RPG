using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestTRacker
{
    public int totalTrees;
    public int killedTrees = 0;
    public bool isActive;
    public string title;
    public string description;
    public int goldReward;

    // Metod f�r att uppdatera antalet d�dade tr�d
    public void UpdateKilledTrees()
    {
        killedTrees++;
        Debug.Log("Trees Destroyed: " + killedTrees + "/" + totalTrees);
    }
}
