using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Titan_Mouvement : Photon.MonoBehaviour {

    private Animator m_chan;
    public NavMeshAgent nav;

    //-----------
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private GameObject listWayPoints;
    //--------------


    // Use this for initialization
    void Start () {
        m_chan = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        listWayPoints = GameObject.Find("Waypoints");
        nav.SetDestination(listWayPoints.transform.GetChild((int)(Random.Range(0, (float)(listWayPoints.transform.childCount)))).position);
        //--------
        //latestDirectionChangeTime = 0f;
        //calcuateNewMovementVector();
        //--------
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f),0, Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    // Update is called once per frame
    void Update () {
        
        //nav.SetDestination(GameObject.Find("Player 1").transform.position);
        
        //float vertical = Input.GetAxis("Vertical");
        m_chan.SetFloat("Input", ((nav.speed * 100f) / 3.5f));
        float dist = nav.remainingDistance;
        Debug.Log(nav.remainingDistance);
        Debug.Log(nav.pathStatus == NavMeshPathStatus.PathComplete);
        Debug.Log(dist != Mathf.Infinity);

        if (dist != Mathf.Infinity && nav.pathStatus == NavMeshPathStatus.PathComplete && nav.remainingDistance == 0)
        {
            nav.SetDestination(listWayPoints.transform.GetChild((int)(Random.Range(0, (float)(listWayPoints.transform.childCount)))).position);
        }

        //---------
        //if the changeTime was reached, calculate a new movement vector
        /*if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime),0,
        transform.position.y + (movementPerSecond.y * Time.deltaTime));*/

            //---------
    }
}
