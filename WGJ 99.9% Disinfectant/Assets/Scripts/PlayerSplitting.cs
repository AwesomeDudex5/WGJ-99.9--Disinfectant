using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplitting : MonoBehaviour
{
	public GameObject playerSpritePrefab;
	
	[SerializeField]
	private float splitScaleDivider = 2.0f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<PlayerGrowth>().isGrowing)
    	{
    		transform.localScale = new Vector3(transform.localScale.x / splitScaleDivider, transform.localScale.y / splitScaleDivider, 1);
    		var newSpriteGO = Instantiate(playerSpritePrefab, transform.position, transform.rotation, transform);
    	}
    }
}
