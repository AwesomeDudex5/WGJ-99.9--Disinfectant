using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
    private const float GROWTH_DURATION = 2.0f;
    private float growthSpeed;
    private Vector3 targetScale;
    private Transform spriteTransform;
    public bool isGrowing;

    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform.GetChild(0).GetComponent<Transform>();
        isGrowing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrowing)
        {
            Vector3 newScale = new Vector3(transform.localScale.x + growthSpeed * Time.deltaTime, transform.localScale.y + growthSpeed * Time.deltaTime, 1);
            transform.localScale = newScale;

            if (transform.localScale.x >= targetScale.x && transform.localScale.y >= targetScale.y)
            {
                isGrowing = false;
                transform.localScale = targetScale;
            }
        }
    }

    void eatAndGrow(float ammount)
    {
        GameManager.current.increasePercentage(ammount);
        isGrowing = true;
        targetScale = new Vector3(transform.localScale.x + ammount, transform.localScale.y + ammount, 1);
        growthSpeed = (targetScale.x - transform.localScale.x) / GROWTH_DURATION;
    }

    void shrink(float ammount)
    {
        GameManager.current.decreasePercentage(ammount);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cell")
        {
            Transform other = col.GetComponent<Transform>();
            if (transform.localScale.x >= other.localScale.x && transform.localScale.y >= other.localScale.y)
            {
                eatAndGrow(other.localScale.x / transform.localScale.x / transform.childCount);
                Destroy(col.gameObject);
            }
        }
        if(col.tag == "Anitbody")
        {
            //calculate ammount here
        }
    }
}
