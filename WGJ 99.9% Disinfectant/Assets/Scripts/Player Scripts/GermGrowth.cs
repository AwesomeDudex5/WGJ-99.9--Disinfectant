using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GermGrowth : MonoBehaviour
{
    public AudioClip germEatSound;
    public AudioClip germDamagedSound;
    private AudioSource _audioSource;

    private const float GROWTH_DURATION = 0.7f;
    private const float SHRINK_DURATION = 2f;
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
        _audioSource = this.GetComponent<AudioSource>();

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

        if (this.tag == "Player")
        {
            GameManager.current.updateMass(this.transform.localScale.x);

            if (transform.localScale.x <= 0)
            {
                GameManager.current.gameOverTrigger();
            }
        }

        if(this.tag == "Germ" && this.transform.localScale.x <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    void eatAndGrow(float ammount)
    {
        //play sound
<<<<<<< HEAD
       // _audioSource.clip = germEatSound;
       // _audioSource.Play();
=======
//        _audioSource.clip = germEatSound;
//        _audioSource.Play();
>>>>>>> 718582fa3fb5b37d124e06aefac012d31d222af3

        GameManager.current.increasePercentage(ammount);
        isGrowing = true;
        targetScale = new Vector3(ammount, ammount, 1);
        growthSpeed = (targetScale.x - transform.localScale.x) / GROWTH_DURATION;

    }

    void shrink(float ammount)
    {
        //play sound
<<<<<<< HEAD
        //_audioSource.clip = germDamagedSound;
       // _audioSource.Play();
=======
//        _audioSource.clip = germDamagedSound;
//        _audioSource.Play();
>>>>>>> 718582fa3fb5b37d124e06aefac012d31d222af3

        GameManager.current.decreasePercentage(ammount);
        isShrinking = true;
        targetScale = new Vector3(ammount, ammount, 1);
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
        else if (rb.bodyType == RigidbodyType2D.Dynamic && (col.tag == "Cell" || col.tag == "Antibodies"))
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
