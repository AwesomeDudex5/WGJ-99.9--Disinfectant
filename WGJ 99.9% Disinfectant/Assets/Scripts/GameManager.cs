using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseText;
    public Text gameOverText;
    public Text infectionPercentageText;
    public SpriteRenderer infectionBG;
    private Color infectionColor;
    public float currentInfectionPercent;
    public SpawnManager spawnManager;

    [SerializeField] private float antibodyPercentStart;
    private bool isPaused;

    public static GameManager current;

    private void Awake()
    {
        current = this;
        Time.timeScale = 1;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        infectionPercentageText.text = "Infection: " + "0.00%";
        infectionColor = infectionBG.color;
        gameOverText.gameObject.SetActive(false);
        pauseText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        infectBackground();
        if (currentInfectionPercent >= antibodyPercentStart)
        {
            Debug.Log("START THE ANTIBODIES");
            spawnManager.initializeAntibodies = true;
        }
        if (currentInfectionPercent <= 0)
        {
            gameOverTrigger();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseGame();
            }
            else
            {
                resumeGame();
            }
        }

        //  Debug.Log("Current Infection: " + currentInfectionPercent);
    }

    public void increasePercentage(float amount)
    {
        currentInfectionPercent += amount / 10;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
        infectionPercentageText.text = "Infection: " + currentInfectionPercent + "%";
    }

    public void decreasePercentage(float amount)
    {
        currentInfectionPercent -= amount;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
        infectionPercentageText.text = "Infection: " + currentInfectionPercent + "%";
    }

    void infectBackground()
    {
        infectionColor = infectionBG.color;
        infectionColor.a = currentInfectionPercent / 100;
        infectionBG.color = infectionColor;
    }

    public void gameOverTrigger()
    {
        gameOverText.gameObject.SetActive(true);

        if (currentInfectionPercent >= 100)
        {
            gameOverText.text = "100% Infected";
        }

        if (currentInfectionPercent <= 0)
        {
            gameOverText.text = "100% Disinfected";
        }

        Time.timeScale = 0;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pauseText.gameObject.SetActive(true);
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseText.gameObject.SetActive(false);
    }

}
