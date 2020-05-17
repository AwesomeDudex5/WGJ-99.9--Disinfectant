using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
    	// Notify to handle trigger in parent Player game object
    	transform.parent.GetComponent<PlayerGrowth>().OnTriggerEnter2D(col);
    }
}
