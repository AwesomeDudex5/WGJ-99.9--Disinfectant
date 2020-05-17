using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum aiType { germ, cell, antibody }

public class AiMovement : MonoBehaviour
{
    public aiType _aiType;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float wanderRange;
    [SerializeField] private float wanderDuration;
    private GameObject[] playerObjects;
    private Transform targetPlayerObject;
    private Vector3 targetPosition;

    bool isWandering;
    bool isRunningAway;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        switch (_aiType)
        {
            case aiType.germ:
                wander();
                break;
            case aiType.cell:
                if (inPlayerRange())
                {
                    runAway();
                }
                else
                {
                    wander();
                }
                break;
            case aiType.antibody:
                if (inPlayerRange())
                {
                    attack();
                }
                else
                {
                    wander();
                }
                break;
            default:
                break;
        }

    }

    private void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    bool inPlayerRange()
    {
        bool inRange = false;
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (Vector2.Distance(this.transform.position, playerObjects[i].transform.position) <= detectionRange)
            {
                inRange = true;
                targetPlayerObject = playerObjects[i].transform;
                Debug.Log("InRange");
            }

        }
        return inRange;
    }

    void wander()
    {
        if (this.transform.position == targetPosition)
        {
            isWandering = false;
        }

        if (!isWandering)
        {
            isWandering = true;
            StartCoroutine(startWander());
        }
    }

    IEnumerator startWander()
    {
        float randomX = Random.Range(-wanderRange, wanderRange);
        float randomY = Random.Range(-wanderRange, wanderRange);
        targetPosition = new Vector3(randomX, randomY, 0f);

        yield return new WaitForSeconds(wanderDuration);

        isWandering = false;
    }

    void runAway()
    {
        if (this.transform.position == targetPosition)
        {
            isRunningAway = false;
        }

        if (!isRunningAway)
        {
            isRunningAway = true;
            StartCoroutine(startRunningAway());
        }

        Debug.Log("Player Position: " + targetPlayerObject.position + " | TargetPosition: " + targetPosition);

    }

    IEnumerator startRunningAway()
    {
        //look away from player
        transform.rotation = Quaternion.LookRotation(transform.position - targetPlayerObject.position);
        
        targetPosition = transform.position + transform.forward * detectionRange;

        //reset rotation
        transform.rotation = Quaternion.identity;

        yield return new WaitForSeconds(wanderDuration);
        isRunningAway = false;
    }

    void attack()
    {
        targetPosition = targetPlayerObject.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       /* if(col.tag == "Player" && _aiType == aiType.antibody)
        {

        }
        */
    }
}
