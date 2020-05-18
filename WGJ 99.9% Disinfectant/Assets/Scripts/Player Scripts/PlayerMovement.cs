using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float moveSpeed;
    private Vector3 mousePosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10f);
        mainCamera.orthographicSize = transform.localScale.x * 5;
        mousePosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        mousePosition.z = 1f;
        this.transform.position = Vector2.Lerp(this.transform.position, mainCamera.ScreenToWorldPoint(mousePosition), moveSpeed / this.transform.localScale.x * Time.deltaTime);
    }
}
