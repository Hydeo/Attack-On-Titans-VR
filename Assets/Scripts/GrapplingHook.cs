using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrapplingHook : MonoBehaviour {

    public Transform camPosition;
    public RaycastHit hit;
    public Material[] helperMaterials = new Material[2];
    public HookPhysicsBehavior.Side side;

    public LayerMask cullingmask;
    public int maxDistance;
    public bool isFlying;
    public Vector3 loc = Vector3.zero;

    public float speed = 10f;
    public float rotationStep = 1f;
    private float lastMagnitude = 0;
 
    public float minDiffForSlowingDown;
    public int yHistorySize;
    private List<float> yAxisHistory;
    public Transform grip1;


    public LineRenderer wire1;
    private LineRenderer helper;

    private float cooldownRope;

    private VRTK_BodyPhysics bp;
    private Transform playArea;
    private HookPhysicsBehavior hpb;
    private bool hasUnclikTrigger = true;
    private AudioSource[] soundEffects;

	// Use this for initialization
	void Start () {
        playArea = VRTK_DeviceFinder.PlayAreaTransform();
        GameObject pa = GameObject.FindGameObjectWithTag("PlayArea");
        bp = pa.GetComponent<VRTK_BodyPhysics>();
        cooldownRope = Time.time;
        helper = GetComponent<LineRenderer>();
        yAxisHistory = new List<float>();
        //camPosition = VRTK_DeviceFinder.HeadsetTransform();

        hpb = pa.GetComponent<HookPhysicsBehavior>();
        soundEffects = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<VRTK_ControllerEvents>().triggerClicked && !isFlying && Time.time - cooldownRope > 0.1f && hasUnclikTrigger)
        {

            FindSpot();
            cooldownRope = Time.time;
        }

        if (GetComponent<VRTK_ControllerEvents>().triggerClicked)
            hasUnclikTrigger = false;
        else
            hasUnclikTrigger = true;

        if (GetComponent<VRTK_ControllerEvents>().triggerTouched)
        {
            
            if(Physics.Raycast(camPosition.position, camPosition.forward, out hit, maxDistance, cullingmask))
            {
                helper.SetPosition(0, camPosition.position);
                helper.SetPosition(1, hit.point);
                helper.material = new Material(helperMaterials[1]);
                helper.enabled = true;
            }
            else
            {
                helper.SetPosition(0, camPosition.position);
                helper.SetPosition(1, camPosition.forward*maxDistance);
                helper.material = new Material(helperMaterials[0]);
                helper.enabled = true;
            }

        }
        else
        {
            helper.enabled = false;
        }

        if (GetComponent<VRTK_ControllerEvents>().triggerClicked && isFlying && Time.time - cooldownRope > 0.1f)
        {
            soundEffects[1].Stop();
            soundEffects[2].Play();
            isFlying = false;
            hpb.SetFlyingToFalse(side);
            wire1.enabled = false;
            cooldownRope = Time.time;
        }

        if (isFlying)
        {
            Flying();
        }

        
	}

    public void FindSpot()
    {

        Debug.Log("Fire raycast");
        //Debug.DrawRay(camPosition.position, camPosition.forward, Color.green);
        if (Physics.Raycast(camPosition.position, camPosition.forward, out hit, maxDistance, cullingmask))
        {
            Debug.Log("Found position");
            soundEffects[0].Play();
            isFlying = true;
            loc = hit.point;
            wire1.enabled = true;
            wire1.SetPosition(1, loc);
            hpb.SetHook(side, loc);
        }
    }

    public void Flying()
    {
        //==========
        if(!soundEffects[1].isPlaying)
            soundEffects[1].Play();
        playArea = VRTK_DeviceFinder.PlayAreaTransform();
        
        //Vector3 currentVelocity = bp.GetVelocity();
        //bp.ApplyBodyVelocity((currentVelocity+(loc - transform.position)).normalized*speed, true, true);
        //==========
        
        wire1.SetPosition(0,grip1.position);

        /*if (checkIfShouldSlow())
        {
            bp.enabled = false;
        }*/

        //If close to the end, stop the rope sound which could be annoying

        if(Vector3.Distance(playArea.position, loc) < 4f)
        {
            soundEffects[1].Stop();
        }

        if (Vector3.Distance(playArea.position, loc) < 1f)
        {
            soundEffects[1].Stop();
            soundEffects[2].Play();
            isFlying = false;
            hpb.SetFlyingToFalse(side);
            wire1.enabled = false;
        }
    }

}
