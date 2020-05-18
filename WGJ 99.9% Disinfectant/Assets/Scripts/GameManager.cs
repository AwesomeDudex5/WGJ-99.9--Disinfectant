using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer infectionBG;
    private Color infectionColor;
    public Text infectionPercentageText;
    public float currentInfectionPercent;
    public SpawnManager spawnManager;

    [SerializeField] private float antibodyPercentStart;

    public static GameManager current;

    private void Awake()
    {
        current = this;
        infectionColor = infectionBG.color;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100f) / 100f;
        // infectionPercentageText.text = currentInfectionPercent + "%";

        infectBackground();
        if (currentInfectionPercent >= antibodyPercentStart)
        {
            spawnManager.initializeAntibodies = true;
        }

        //  Debug.Log("Current Infection: " + currentInfectionPercent);
    }

    public void increasePercentage(float amount)
    {
        currentInfectionPercent += amount / 10;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
    }

    public void decreasePercentage(float amount)
    {
        currentInfectionPercent -= amount / 10;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
    }

    void infectBackground()
    {
        infectionColor = infectionBG.color;
        infectionColor.a = currentInfectionPercent / 100;
        infectionBG.color = infectionColor;
    }

}
