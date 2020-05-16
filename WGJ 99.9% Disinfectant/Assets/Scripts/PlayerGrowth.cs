using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrowth : MonoBehaviour
{
	[SerializeField]
	private float growthRate = 1.0f;
	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void grow()
    {
    	transform.localScale = new Vector3(transform.localScale.x * growthRate, transform.localScale.y * growthRate, 1);
    }
}
