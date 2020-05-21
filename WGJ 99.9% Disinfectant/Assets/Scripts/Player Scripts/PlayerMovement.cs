using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float cameraDistance;
    public float moveSpeed;
    private Vector3 mousePosition;
    private Rigidbody2D rb;

    private GermGrowth germGrowth;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        germGrowth = GetComponent<GermGrowth>();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        if (!germGrowth.isShrinking)
        {
            mainCamera.orthographicSize = transform.localScale.x * cameraDistance;
        }
        mousePosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        //mousePosition.z = 1f;
        this.transform.position = Vector2.Lerp(this.transform.position, mainCamera.ScreenToWorldPoint(mousePosition), moveSpeed / this.transform.localScale.x * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col) {
        // Only use dynamic body with borders, kinematic with cell and antibodies
        var rb = GetComponent<Rigidbody2D>();
        if (col.tag == "Border")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (rb.bodyType == RigidbodyType2D.Dynamic && (col.tag == "Cell" || col.tag == "Antibodies") )
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
