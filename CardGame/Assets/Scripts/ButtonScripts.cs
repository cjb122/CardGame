using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScripts : MonoBehaviour
{
    private int next = 1;    

    public void exit()
    {
        Application.Quit();
    }

    public void exitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void aboutGame()
    {
        SceneManager.LoadScene(2);
    }

    public void nextText()
    {
        Animator a = FindObjectOfType<Animator>();
        a.SetInteger("NextText", next);
        next += 1;
    }
}
