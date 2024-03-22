using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiverScript : MonoBehaviour
{
    public TMP_Text questText; // Referens till textrutan som visar quest-informationen
    public QuestTRacker questTracker; // Referens till quest-objektet

    private void Start()
    {
        TreeDestroy.OnTreeDestroyed += UpdateQuest; // Prenumerera p� eventet f�r n�r ett tr�d f�rst�rs
        CountObjects();
        UpdateQuestText();
        
    }

    private void CountObjects()
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        questTracker.totalTrees = trees.Length;
      
        Debug.Log("Total Trees: " + questTracker.totalTrees);
    }

    private void UpdateQuest()
    {
        questTracker.killedTrees++; // �ka antalet f�rst�rda tr�d
        UpdateQuestText(); // Uppdatera UI:t med den nya informationen
    }

    private void UpdateQuestText()
    {
        string questStatus = questTracker.killedTrees + "/" + questTracker.totalTrees; // Skapa str�ng f�r att visa questens framsteg
        string questInfo = "Quest: " + questTracker.title + "\nDescription: " + questTracker.description + "\nProgress: " + questStatus; // Sammans�tt texten f�r quest-dialogen
        questText.text = questInfo; // Uppdatera texten i textrutan
    }
}
