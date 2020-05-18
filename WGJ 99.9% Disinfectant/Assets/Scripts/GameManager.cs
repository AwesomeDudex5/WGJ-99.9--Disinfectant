using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text infectionPercentageText;
    public float currentInfectionPercent;

    public static GameManager current;

    private void Awake()
    {
        current = this;
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
    }

    public void increasePercentage(float amount)
    {
        currentInfectionPercent += amount/10;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
    }

    public void decreasePercentage(float amount)
    {
        currentInfectionPercent -= amount / 10;
        currentInfectionPercent = Mathf.Round(currentInfectionPercent * 100) / 100;
    }

}
