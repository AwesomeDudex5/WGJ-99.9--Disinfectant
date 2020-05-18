using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GermGrowth : MonoBehaviour
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
		if (isGrowing) {
			Vector3 newScale = new Vector3(transform.localScale.x + growthSpeed * Time.deltaTime, transform.localScale.y + growthSpeed * Time.deltaTime, 1);
			transform.localScale = newScale;
    		
			if (transform.localScale.x >= targetScale.x && transform.localScale.y >= targetScale.y) {
				isGrowing = false;
				transform.localScale = targetScale;
			}
		}
	}
    
	void eatAndGrow(float target)
	{
		isGrowing = true;
		targetScale = new Vector3(target, target, 1);
		growthSpeed = (targetScale.x - transform.localScale.x) / GROWTH_DURATION;
	}
    
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cell")
        {
            Transform other = col.GetComponent<Transform>();
            if (transform.localScale.x >= other.localScale.x && transform.localScale.y >= other.localScale.y)
            {
                eatAndGrow(other.localScale.x + transform.localScale.x);
                Destroy(col.gameObject);
            }
        }
    }
}
