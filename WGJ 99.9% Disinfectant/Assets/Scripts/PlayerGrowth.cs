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
    		Vector3 newScale = new Vector3(spriteTransform.localScale.x + growthSpeed * Time.deltaTime, spriteTransform.localScale.y + growthSpeed * Time.deltaTime, 1);
    		spriteTransform.localScale = newScale;
    		
    		if (spriteTransform.localScale.x >= targetScale.x && spriteTransform.localScale.y >= targetScale.y)
    		{
    			isGrowing = false;
    			spriteTransform.localScale = targetScale;
    		}
    	}
    }
    
    void eatAndGrow()
    {
    	isGrowing = true;
    	targetScale = new Vector3(spriteTransform.localScale.x * growthScaleMultiplier, spriteTransform.localScale.y * growthScaleMultiplier, 1);
    	growthSpeed = (targetScale.x - spriteTransform.localScale.x) / GROWTH_DURATION;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
    	Transform other = col.transform.GetChild(0).GetComponent<Transform>();
    	if (spriteTransform.localScale.x > other.localScale.x && spriteTransform.localScale.y > other.localScale.y)
    		eatAndGrow();
    	Destroy(col.gameObject);
    }
}
