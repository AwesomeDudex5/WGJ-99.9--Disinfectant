using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerGrowth : MonoBehaviour
{
	[SerializeField]
	private float growthScaleMultiplier = 1.0f;
	
	[SerializeField]
	private bool isGrowing = false;
	private Vector3 targetScale;
	private Transform spriteTransform;
	private const float GROWTH_DURATION = 2.0f;
	private float growthSpeed;
	
    // Start is called before the first frame update
    void Start()
    {
    	spriteTransform = transform.GetChild(0).GetComponent<Transform>();
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
    
    void eatAndGrow()
    {
    	isGrowing = true;
    	targetScale = new Vector3(transform.localScale.x * growthScaleMultiplier, transform.localScale.y * growthScaleMultiplier, 1);
    	growthSpeed = (targetScale.x - transform.localScale.x) / GROWTH_DURATION;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
    	Transform other = col.GetComponent<Transform>();
    	if (transform.localScale.x >= other.localScale.x && transform.localScale.y >= other.localScale.y)
    	{
    		eatAndGrow();
    		Destroy(col.gameObject);
    	}
    }
}
