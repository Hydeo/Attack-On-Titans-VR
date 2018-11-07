using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SpawnSword : Photon.MonoBehaviour {
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

        if (CanGrab(grabbingController) && Time.time >= spawnDelayTimer)
        {
            GameObject newSword = PhotonNetwork.Instantiate("AOT_Sword", grabbingController.transform.position,grabbingController.transform.rotation,0);
            newSword.GetComponentInParent<VRTK_InteractableObject>().isGrabbable = true ;
            newSword.name = "swordClone";
            grabbingController.GetComponent<VRTK_InteractTouch>().ForceTouch(newSword);
            grabbingController.AttemptGrab();
            spawnDelayTimer = Time.time + spawnDelay;
            //photonView.RPC("NetFire", PhotonTargets.All, newArrow.transform.position, newArrow.transform.rotation);
        }
    }

    [PunRPC]
    void NetFire(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Create Arrow PUN");
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate(
            swordPrefab,
            position,
            rotation);
        // Play sound of gun shooting
        //AudioSource.PlayClipAtPoint(fireGunSound, transform.position, 1.0f);
        // Play animation of gun shooting
        //fireAnimation.Play();
    }

    private bool CanGrab(VRTK_InteractGrab grabbingController)
    {
        return (grabbingController && grabbingController.GetGrabbedObject() == null && grabbingController.IsGrabButtonPressed());
    }

}
