using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class todelete : MonoBehaviour {

    public GameObject a1;
    public GameObject a2;
    private bool done = false;
    private Vector3 from;
	// Use this for initialization
	void Start () {

        from = a2.transform.position - transform.position;


    }
	
	// Update is called once per frame
	void Update () {

       // if (done == false)
      //  {
            Debug.Log("TESTTESTESTESTSET");
            Vector3 targetDir = a1.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = 0.1f * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(from, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red,10000f);
            from = newDir;
            // Move our position a step closer to the target.
            //transform.rotation = Quaternion.LookRotation(newDir);
            done = true;
      //  }
    }
}
