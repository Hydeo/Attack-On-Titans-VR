using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GrapplingHook : MonoBehaviour {

    public Transform camPosition;
    public RaycastHit hit;

    public LayerMask cullingmask;
    public int maxDistance;
    public bool isFlying;
    public Vector3 loc;

    public float speed = 10f;
    public Transform grip1;
    public Transform grip2;

    public LineRenderer wire1;
    public LineRenderer wire2;

    private Transform playArea;

	// Use this for initialization
	void Start () {
        playArea = VRTK_DeviceFinder.PlayAreaTransform();
        
        //camPosition = VRTK_DeviceFinder.HeadsetTransform();

    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GetComponent<VRTK_ControllerEvents>().triggerClicked);

        if (Input.GetKey(KeyCode.K) || GetComponent<VRTK_ControllerEvents>().triggerClicked)
            FindSpot();

        if (isFlying)
            Flying();

        if (Input.GetKey(KeyCode.L) && isFlying)
        {
            isFlying = false;
            wire1.enabled = false;
            wire2   .enabled = false;
        }
	}

    public void FindSpot()
    {
        /*if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag == "Tagged")
            {
                Debug.DrawRay(transform.position, transform.forward, Color.green);
                print("Hit");
            }
        }*/

        Debug.Log("Fire raycast");
        Debug.DrawRay(camPosition.position, camPosition.forward, Color.green);
        if (Physics.Raycast(camPosition.position, camPosition.forward, out hit, maxDistance, cullingmask))
        {
            Debug.Log("Found position");
            isFlying = true;
            loc = hit.point;
            wire1.enabled = true;
            wire1.SetPosition(1, loc);
            wire2.enabled = true;
            wire2.SetPosition(1, loc);

        }
    }

    public void Flying()
    {
        Debug.Log("IsFlyyyiiiiinnng");
        //transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        //VRTK_SDKManager sdk = VRTK_SDKManager.instance;
        //sdk.loadedSetup.actualBoundaries.transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));

        playArea = VRTK_DeviceFinder.PlayAreaTransform();
        playArea.position = Vector3.Lerp(playArea.position, loc, speed * Time.deltaTime / Vector3.Distance(playArea.position, loc));
        wire1.SetPosition(0,grip1.position);
        wire2.SetPosition(0,grip2.position);

        if (Vector3.Distance(playArea.position, loc) < 0.5f)
        {
            isFlying = false;
            wire1.enabled = false;
            wire2.enabled = false;
        }
    }

}
