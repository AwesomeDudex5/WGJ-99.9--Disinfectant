using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerGrowth : MonoBehaviour
{
	[SerializeField]
	private float growthRate = 1.0f;
	
	[SerializeField]
	private bool isGrowing = false;
	private Vector3 targetScale;
	[SerializeField]
	private float growthSpeed = 1.0f;
	
    // Start is called before the first frame update
    void Start()
    {
    	
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
    
    void grow()
    {
    	isGrowing = true;
    	targetScale = new Vector3(transform.localScale.x * growthRate, transform.localScale.y * growthRate, 1);
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
    	grow();
    	Destroy(col.gameObject);
    }
}
