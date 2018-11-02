using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Sheath : Photon.MonoBehaviour
{
    public GameObject bladePrefab;


    private bool cooldownOk = true;
    private GameObject lastForged;
    
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("STAYYY");
        VRTK_InteractGrab grabbingObject = (other.gameObject.GetComponent<VRTK_InteractGrab>() ? other.gameObject.GetComponent<VRTK_InteractGrab>() : other.gameObject.GetComponentInParent<VRTK_InteractGrab>());
        
        if (canGrab(grabbingObject))
        {
            Debug.Log("Spawn Object");

            //GameObject spawnBlade = Instantiate(bladePrefab);
            //GameObject spawnBlade = PhotonNetwork.Instantiate("Sword", this.transform.position, this.transform.rotation,0);

            int id1 = PhotonNetwork.AllocateViewID();

            PhotonView photonView = this.GetComponent<PhotonView>();
            
            photonView.RPC("SpawnOnNetwork", PhotonTargets.AllBuffered, this.transform.position, this.transform.rotation, id1);
            cooldownOk = false;
            lastForged.GetComponents<VRTK_InteractableObject>();
            grabbingObject.GetComponent<VRTK_InteractTouch>().ForceTouch(lastForged);
            grabbingObject.AttemptGrab();
            //photonView.RPC("NetFire", PhotonTargets.All);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OUT OF IT !!!!!");
        cooldownOk = true;
    }

    [PunRPC]
    void SpawnOnNetwork(Vector3 pos, Quaternion rot, int id1)
    {
        GameObject newBlade = Instantiate(bladePrefab, pos, rot) ;
        lastForged = newBlade;
        // Set player's PhotonView
        PhotonView[] nViews = newBlade.GetComponentsInChildren<PhotonView>();
        nViews[0].viewID = id1;

    }



    private bool canGrab(VRTK_InteractGrab grabbingObject)
    {
        //Debug.Log(grabbingObject.gameObject.GetComponent<VRTK_ControllerEvents>().gripClicked);
        return (grabbingObject && grabbingObject.GetGrabbedObject() == null && grabbingObject.IsGrabButtonPressed() && cooldownOk);
    }
}
