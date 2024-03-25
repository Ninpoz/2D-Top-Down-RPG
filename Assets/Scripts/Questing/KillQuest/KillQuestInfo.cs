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
        EnemyHealth.OnEnemyKilled += UpdateQuest; // Prenumerera på eventet för när ett träd förstörs
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
        questTracker.killedEnemies++; // Öka antalet förstörda träd
        UpdateQuestText(); // Uppdatera UI:t med den nya informationen
    }

    private void UpdateQuestText()
    {
        string questStatus = questTracker.killedEnemies + "/" + questTracker.totalEnemies; // Skapa sträng för att visa questens framsteg
        string questInfo = "Quest: " + questTracker.title + "\nDescription: " + questTracker.description + "\nProgress: " + questStatus; // Sammansätt texten för quest-dialogen
        questText.text = questInfo; // Uppdatera texten i textrutan
    }
}
