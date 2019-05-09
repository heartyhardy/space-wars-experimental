using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadEndGame()
    {
        StartCoroutine(WaitAndLoad());       
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameOver");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
