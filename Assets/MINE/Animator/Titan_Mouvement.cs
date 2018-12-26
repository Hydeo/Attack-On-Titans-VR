using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Titan_Mouvement : Photon.MonoBehaviour {

    private Animator m_chan;
    public NavMeshAgent nav;
    public float walkingSpeed;
    public float runningSpeed;
    //-----------
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private GameObject listWayPoints;
    private TitanAggro ta;
    //--------------
    private GameObject sphereAttack = null;

    // Use this for initialization
    void Start () {
        m_chan = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        listWayPoints = GameObject.Find("Waypoints");
        nav.SetDestination(listWayPoints.transform.GetChild((int)(Random.Range(0, (float)(listWayPoints.transform.childCount)))).position);
        ta = GetComponent<TitanAggro>();
    }

    public void navStop()
    {
        nav.Stop();
    }

    public void navStart()
    {
        nav.Resume();
    }
    // Update is called once per frame
    public void Wander()
    {
        nav.SetDestination(listWayPoints.transform.GetChild((int)(Random.Range(0, (float)(listWayPoints.transform.childCount)))).position);
    }

    void Update () {
        Debug.Log("TARGET : " + ta.target);
        m_chan.SetFloat("Input", ((nav.speed * 100f) / 3.5f));
        if (ta.target == null) {
            nav.speed = walkingSpeed;
            float dist = nav.remainingDistance;
            Debug.Log(transform.gameObject.name + " - " + nav.destination.ToString() + " => " + nav.remainingDistance.ToString() +" = "+nav.pathStatus);

                if (!nav.pathPending && !nav.hasPath)
                {
                    Debug.Log("I have reached my destination!");
                // Code that you want to execute when this event occurs.
                Wander();
            }
               
            
        }
        else
        {
            nav.speed = runningSpeed;
            nav.SetDestination(ta.target.position);
        }

        


    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" collide in titna mouvement");
        
    }*/
}
