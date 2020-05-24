using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    public enum State
    {
        Play,
        GameOver,
        Pause,
        PlayDelay
    }

    private float timer;
    private bool isPause;
    public State state { get; private set; }
    public GameObject menu;
    public GameObject hp;
    public Player player;

    public int delayToStartSeconds;
    public TMPro.TextMeshProUGUI startCounter;
    //public TMPro.TextMeshProUGUI timerText;
    private float counterTextSize;

    private void Start()
    {
        AudioListener.volume = MainMenuController.value/100;
        hp.SetActive(true);
        menu.SetActive(false);
        counterTextSize = startCounter.fontSize;
        Play();
        player.PlayerDestroyedEvent += GameOver;
    }

    private void Awake()
    {
        if (Instance ==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {


        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && isPause == false && state != State.PlayDelay && state != State.GameOver)
        {
            isPause = true;
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause == true)
        {
            isPause = false;
            PlayafterPause();
        }
        if (isPause == true)
        {
            timer = 0;
        }
        else if (isPause == false)
        {
            timer = 1f;
        }

        if (state == State.GameOver)
        {
            timer = 0;
        }

    }
    public void Play()
    {
        state = State.PlayDelay;
        startCounter.gameObject.SetActive(true);
        StartCoroutine(DelayToStart());
    }


    public void PlayafterPause()
    {
        state = State.Play;

        menu.SetActive(false);
    }

    public void GameOver()
    {
        hp.SetActive(false);
        menu.SetActive(true);
        state = State.GameOver;
    }

    public void Pause()
    {
        state = State.Pause;
        menu.SetActive(true);
    }
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }



    private IEnumerator DelayToStart()
    {
        float timer = delayToStartSeconds;
       
        startCounter.SetText(timer.ToString());
        float timeUpdate = Time.time;

        while (timer > 0)
        {
            startCounter.fontSize = counterTextSize * (1 - (Time.time - timeUpdate));
            if ((Time.time - timeUpdate) >= 1)
            {
                timer -= 1;
                timeUpdate = Time.time;
                startCounter.SetText(timer.ToString());
            }

            yield return null;
        }

        state = State.Play;

        startCounter.gameObject.SetActive(false);
        // CheckTime();
    }



}
