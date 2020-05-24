using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject panel;
    public Slider slider;
    public static float value = 10f;

    private void Start()
    {
        panel.SetActive(false);
        slider.value = value;
    }

    private void Update()
    {
        value = slider.value;
        AudioListener.volume = value/100;
        
    }


    public void ToMainScene()
    {
        
        SceneManager.LoadScene("Levels");
   
    
    }



    public void Quit()
    {
        Application.Quit();

            }

}
