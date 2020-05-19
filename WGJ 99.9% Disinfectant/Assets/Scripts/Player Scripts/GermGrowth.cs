using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermGrowth : MonoBehaviour
{
    private const float GROWTH_DURATION = 0.7f;
    private float growthSpeed;
    private Vector3 targetScale;
    private Transform spriteTransform;
    public bool isGrowing;
    public bool isShrinking;
    private Vector3 newScale;
    public Text playerScaleText;

    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform.GetChild(0).GetComponent<Transform>();
        isGrowing = false;
        isShrinking = false;
        setPlayerScaleText();
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
            newScale = new Vector3(transform.localScale.x - growthSpeed * Time.deltaTime, transform.localScale.y - growthSpeed * Time.deltaTime, 1);
            transform.localScale = newScale;
            {
                if (transform.localScale.x <= targetScale.x && transform.localScale.y <= targetScale.y)
                {
                    isShrinking = false;
                    transform.localScale = targetScale;
                }
            }
        }
        
        setPlayerScaleText();
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
        growthSpeed = (transform.localScale.x - targetScale.x) / GROWTH_DURATION;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cell")
        {
            Transform other = col.GetComponent<Transform>();
            if (transform.localScale.x >= other.localScale.x && transform.localScale.y >= other.localScale.y)
            {
                if (isGrowing)
                {
                    transform.localScale = targetScale;
                }
                eatAndGrow(other.localScale.x / 8 + transform.localScale.x);
                Destroy(col.gameObject);
            }
        }
        if (col.tag == "Antibody")
        {
            if (isShrinking)
            {
                transform.localScale = targetScale;
            }
            shrink((transform.localScale.x - col.transform.localScale.x) / 3);
            Destroy(col.gameObject);
        }
    }
    
    void setPlayerScaleText()
    {
    	if (playerScaleText != null)
    		playerScaleText.text = "Player's scale: " + (int)(transform.localScale.x * 100);
    }
}
