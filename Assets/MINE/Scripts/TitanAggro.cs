using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitanAggro : Photon.MonoBehaviour {

    private TitansFieldOfView fov;
    public Transform target;
    // Use this for initialization
    void Start () {
        fov = GetComponent<TitansFieldOfView>();
	}
	


    private int findIndexOfClosest()
    {
        float min = fov.distTargets[0];
        int index = 0;
        for(int i = 1; i < fov.distTargets.Count; i++)
        {
            if (fov.distTargets[i] < min)
            {
                min = fov.distTargets[i];
                index = i;
            }
        }

        return index;
        
    }
	// Update is called once per frame
	void Update () {

        if (fov.visibleTargets.Count != 0)
            target = fov.visibleTargets[findIndexOfClosest()];
        /*else
            target = target;*/
	}

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collideee");
        if(collision.gameObject.tag == "Arrow")
        {   
            Debug.Log("Hit by " + collision.gameObject.name.Substring(collision.gameObject.name.IndexOf("_")+1  ));
            target = GameObject.Find(collision.gameObject.name.Substring(collision.gameObject.name.IndexOf("_") + 1)).transform;
        }
    }
}
