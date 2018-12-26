using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlayerDeathTrigger : MonoBehaviour {
    private AudioSource aie;
    public Transform destination;
    VRTK_SDKManager sdk;
    // Use this for initialization
    void Start () {
        aie = GetComponent<AudioSource>();
        sdk = VRTK_SDKManager.instance;
        destination = GameObject.Find("PlayerRespawn").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "attackSphere")
        {
            if (!aie.isPlaying)
                aie.Play();
            
            sdk.loadedSetup.actualBoundaries.transform.position = destination.position;
            sdk.loadedSetup.actualBoundaries.transform.rotation = destination.rotation;
        }
    }
}
