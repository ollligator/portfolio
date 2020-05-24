using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public void ToMainScene()
    {
        SceneManager.LoadScene("Main");


    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");


    }
    public void ToLevel1()
    {
        SceneManager.LoadScene("Level1");


    }

    public void ToLevel2()
    {
        SceneManager.LoadScene("Level2");


    }
    public void ToLevel3()
    {
        SceneManager.LoadScene("Level3");


    }
    public void ToLevel4()
    {
        SceneManager.LoadScene("Level4");


    }
}
