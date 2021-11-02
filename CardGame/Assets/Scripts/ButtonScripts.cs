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

    public void startIntro()
    {
        Animator a = FindObjectOfType<Animator>();
        a.SetBool("StartIntro", true);
    }

    public void startGame()
    {
        SceneManager.LoadScene(2);
    }

    public void aboutGame()
    {
        Animator a = FindObjectOfType<Animator>();
        a.SetBool("LoadAbout", true);
    }

    public void returnFromAboutGame()
    {
        Animator a = FindObjectOfType<Animator>();
        a.SetBool("LoadAbout", false);
    }

    public void nextText()
    {
        Animator a = FindObjectOfType<Animator>();
        a.SetInteger("NextText", next);
        next += 1;
    }

    public void setScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
}
