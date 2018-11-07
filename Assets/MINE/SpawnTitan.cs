using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.Controllables.PhysicsBased;

public class SpawnTitan : Photon.MonoBehaviour {
    private bool previousState;
    private VRTK_PhysicsPusher pp;

    public GameObject titanObject;

    // Use this for initialization
    void Start () {
        pp = GetComponent<VRTK_PhysicsPusher>();
        previousState = pp.IsResting();
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(PhotonNetwork.isMasterClient);
        if (previousState == false && pp.IsResting() == true && PhotonNetwork.isMasterClient)
        {
            Debug.Log("Spawn TITAN");
            //photonView.RPC("SpawnBitchies", PhotonTargets.AllBuffered);
            PhotonNetwork.Instantiate("unitychan", new Vector3(-2, 0, 7), new Quaternion(0, 180, 0, 0), 0, new object[] { name });
        }

        previousState = pp.IsResting();

    }

    /*[PunRPC]
    void SpawnBitchies()
    {
        
            Debug.Log("Spawn Bitchies");
            PhotonNetwork.Instantiate("unitychan", new Vector3(-2, 0, 7), new Quaternion(0, 180, 0, 0), 0, new object[] { name });
        
    }*/

   
}
