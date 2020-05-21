using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseText;
    public GameObject gameOverUI;
    private Text gameOverText;
    public Text infectionPercentageText;
    public Text currentMassText;
    private float currentMass;
    public SpriteRenderer infectionBG;
    private Color infectionColor;
    public float currentInfectionPercent;
    public SpawnManager spawnManager;

    [SerializeField] private float antibodyPercentStart;
    public GameObject atibodyWarningUI;
    private int numberOfFlashes = 3;
    private bool warningPlayed;
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
        warningPlayed = false;
        isPaused = false;
        gameOverText = gameOverUI.GetComponent<Text>();
        infectionPercentageText.text = "Infection: " + "0.00%";
        currentMassText.text = "Mass: " + currentMass;
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
            spawnManager.initializeAntibodies = true;
            if(!warningPlayed)
            {
                warningPlayed = true;
                StartCoroutine(displayWarning());
            }
        }
        if (currentInfectionPercent <= 0 || currentInfectionPercent >= 100)
        {
            gameOverTrigger();
        }
       
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void updateMass(float amount)
    {
        currentMass = amount;
        currentMassText.text = "Mass: " + currentMass;
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

    IEnumerator displayWarning()
    {
        for(int i=0; i < numberOfFlashes; i++)
        {
            atibodyWarningUI.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            atibodyWarningUI.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
