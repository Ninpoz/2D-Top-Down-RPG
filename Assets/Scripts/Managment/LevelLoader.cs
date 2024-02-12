using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public float transitionTime = 1;
    public Animator transistion;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextLevel()
    {
       
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //play animation
        transistion.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);

        //loadscene
        SceneManager.LoadScene(levelIndex);
    }
}


