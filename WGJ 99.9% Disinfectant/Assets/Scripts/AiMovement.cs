using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum aiType { germ, cell, antibody }

public class AiMovement : MonoBehaviour
{
    public Animator anim;
    public aiType _aiType;
    public float moveSpeed;
    public float detectionRange;
    public float wanderRange;
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
                    anim.SetBool("RunningAway", true);
                    runAway();
                }
                else
                {
                    anim.SetBool("RunningAway", false);
                    wander();
                }
                break;
            case aiType.antibody:
                if (inPlayerRange())
                {
                    anim.SetBool("Attacking", true);
                    attack();
                }
                else
                {
                    anim.SetBool("Attacking", false);
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
        playerObjects = GameObject.FindGameObjectsWithTag("Germ");
        for (int i = 0; i < playerObjects.Length; i++)
        {
            if (Vector2.Distance(this.transform.position, playerObjects[i].transform.position) <= detectionRange)
            {
                inRange = true;
                targetPlayerObject = playerObjects[i].transform;
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

        //adjust values as needed ************
        float currentScale = this.transform.localScale.x;
        float randomX = Random.Range(this.transform.position.x - wanderRange, this.transform.position.x + wanderRange);
        float randomY = Random.Range(this.transform.position.y - wanderRange, this.transform.position.y + wanderRange);
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

}
