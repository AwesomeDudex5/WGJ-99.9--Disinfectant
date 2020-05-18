using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSplitting : MonoBehaviour
{
    public GameObject germPrefab;
    [SerializeField] private float splitForceAmount;

    private Rigidbody2D rb;
    private Vector2 mousePos;

    [SerializeField]
    private float splitScaleDivider = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && !transform.GetComponentInChildren<GermGrowth>().isGrowing && transform.localScale.x > 1)
        {
            split();
        }
    }

    void split()
    {
        Debug.Log("Splitting");
        transform.localScale = new Vector3(transform.localScale.x / splitScaleDivider, transform.localScale.y / splitScaleDivider, 1);
        GameObject otherGerm = Instantiate(germPrefab, transform.position, Quaternion.identity);
        otherGerm.transform.localScale = this.transform.localScale;

        //Get split facing direction
        Vector2 lookDirection = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        //add force to player object
        rb.AddForce(transform.up * splitForceAmount, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
    }
}
