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
        TreeDestroy.OnTreeDestroyed += UpdateQuest; // Prenumerera på eventet för när ett träd förstörs
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
        questTracker.killedTrees++; // Öka antalet förstörda träd
        UpdateQuestText(); // Uppdatera UI:t med den nya informationen
    }

    private void UpdateQuestText()
    {
        string questStatus = questTracker.killedTrees + "/" + questTracker.totalTrees; // Skapa sträng för att visa questens framsteg
        string questInfo = "Quest: " + questTracker.title + "\nDescription: " + questTracker.description + "\nProgress: " + questStatus; // Sammansätt texten för quest-dialogen
        questText.text = questInfo; // Uppdatera texten i textrutan
    }
}
