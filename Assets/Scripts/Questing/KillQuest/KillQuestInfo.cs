using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillQuestInfo : MonoBehaviour
{
    public TMP_Text questText; // Referens till textrutan som visar quest-informationen
    public KillQuestTRacker questTracker; // Referens till quest-objektet

    private void Start()
    {
        EnemyHealth.OnEnemyKilled += UpdateQuest; // Prenumerera p� eventet f�r n�r ett tr�d f�rst�rs
        CountObjects();
        UpdateQuestText();
        
    }

    private void CountObjects()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        questTracker.totalEnemies = enemy.Length;
      
        Debug.Log("Total Trees: " + questTracker.totalEnemies);
    }

    private void UpdateQuest()
    {
        questTracker.killedEnemies++; // �ka antalet f�rst�rda tr�d
        UpdateQuestText(); // Uppdatera UI:t med den nya informationen
    }

    private void UpdateQuestText()
    {
        string questStatus = questTracker.killedEnemies + "/" + questTracker.totalEnemies; // Skapa str�ng f�r att visa questens framsteg
        string questInfo = "Quest: " + questTracker.title + "\nDescription: " + questTracker.description + "\nProgress: " + questStatus; // Sammans�tt texten f�r quest-dialogen
        questText.text = questInfo; // Uppdatera texten i textrutan
    }
}
