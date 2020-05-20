﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermGrowth : MonoBehaviour
{
    private const float GROWTH_DURATION = 0.7f;
    private const float SHRINK_DURATION = 1f;
    private float growthSpeed;
    private float shrinkSpeed;
    private Vector3 targetScale;
    private Transform spriteTransform;
    public bool isGrowing;
    public bool isShrinking;
    private Vector3 newScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform.GetChild(0).GetComponent<Transform>();
        isGrowing = false;
        isShrinking = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            newScale = new Vector3(transform.localScale.x + growthSpeed * Time.deltaTime, transform.localScale.y + growthSpeed * Time.deltaTime, 1);
            transform.localScale = newScale;

            if (transform.localScale.x >= targetScale.x && transform.localScale.y >= targetScale.y)
            {
                isGrowing = false;
                transform.localScale = targetScale;
            }
        }

        if (isShrinking)
        {
            newScale = new Vector3(transform.localScale.x - shrinkSpeed * Time.deltaTime, transform.localScale.y - shrinkSpeed * Time.deltaTime, 1);
            transform.localScale = newScale;
            {
                if (transform.localScale.x <= targetScale.x && transform.localScale.y <= targetScale.y)
                {
                    isShrinking = false;
                    transform.localScale = targetScale;
                }
            }
        }

        if (transform.localScale.x <= 0.0f && this.tag == "Player")
        {
            GameManager.current.gameOverTrigger();
        }


    }

    void eatAndGrow(float ammount)
    {
        GameManager.current.increasePercentage(ammount);
        isGrowing = true;
        targetScale = new Vector3(ammount, ammount, 1);
        growthSpeed = (targetScale.x - transform.localScale.x) / GROWTH_DURATION;
    }

    void shrink(float ammount)
    {
        GameManager.current.decreasePercentage(ammount);
        isShrinking = true;
        targetScale = new Vector3(ammount, ammount, 1);
        if (targetScale.x < 1)
        {
            Destroy(gameObject);
        }
        shrinkSpeed = (transform.localScale.x - targetScale.x) / SHRINK_DURATION;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // Only use dynamic body with borders, kinematic with cell and antibodies
        var rb = GetComponent<Rigidbody2D>();
        if (col.tag == "Border")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (rb.bodyType == RigidbodyType2D.Dynamic && (col.tag == "Cell" || col.tag == "Antibodies") )
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (col.tag == "Cell")
        {
            Transform other = col.GetComponent<Transform>();
            if (transform.localScale.x >= other.localScale.x && transform.localScale.y >= other.localScale.y)
            {
                eatAndGrow(other.localScale.x / 10 + transform.localScale.x);
                Destroy(col.gameObject);
            }
        }
        if (col.tag == "Antibody" && !isShrinking)
        {
            if (isGrowing)
            {
                isGrowing = false;
            }
            shrink(transform.localScale.x - col.transform.localScale.x / 3);
            Destroy(col.gameObject);
        }

    }

}
