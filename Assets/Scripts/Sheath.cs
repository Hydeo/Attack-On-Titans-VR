using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Sheath : Photon.MonoBehaviour
{
    public GameObject swordPrefab;
    public float spawnDelay = 1f;

    private float spawnDelayTimer = 0f;

    private void Start()
    {
        spawnDelayTimer = 0f;
    }

    private void OnTriggerStay(Collider collider)
    {
        VRTK_InteractGrab grabbingController = (collider.gameObject.GetComponent<VRTK_InteractGrab>() ? collider.gameObject.GetComponent<VRTK_InteractGrab>() : collider.gameObject.GetComponentInParent<VRTK_InteractGrab>());

        if (canGrab(grabbingController) && Time.time >= spawnDelayTimer)
        {
            GameObject newSword = PhotonNetwork.Instantiate("AOT_Sword", grabbingController.transform.position, grabbingController.transform.rotation, 0);
            newSword.GetComponentInParent<VRTK_InteractableObject>().isGrabbable = true;
            newSword.name = "swordClone";
            grabbingController.GetComponent<VRTK_InteractTouch>().ForceTouch(newSword);
            grabbingController.AttemptGrab();
            spawnDelayTimer = Time.time + spawnDelay;
            //photonView.RPC("NetFire", PhotonTargets.All, newArrow.transform.position, newArrow.transform.rotation);
        }
    }



    private bool canGrab(VRTK_InteractGrab grabbingObject)
    {
        //Debug.Log(grabbingObject.gameObject.GetComponent<VRTK_ControllerEvents>().gripClicked);
        return (grabbingObject && grabbingObject.GetGrabbedObject() == null && grabbingObject.IsGrabButtonPressed());
    }
}
