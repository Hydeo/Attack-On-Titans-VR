using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class TeleportPlayer : VRTK_DestinationMarker
{
    private Transform destination;

    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Teleport trigger" + collider.name);
        VRTK_ControllerEvents controller = (collider.GetComponent<VRTK_ControllerEvents>() ? collider.GetComponent<VRTK_ControllerEvents>() : collider.GetComponentInParent<VRTK_ControllerEvents>());
        if (controller != null)
        {
                destination = GameObject.Find("PlayerRespawn").transform;
                float distance = Vector3.Distance(transform.position, destination.position);
                VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
                OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        Debug.Log("Teleport collison " + collider.gameObject.name);
        VRTK_ControllerEvents controller = (collider.gameObject.GetComponent<VRTK_ControllerEvents>() ? collider.gameObject.GetComponent<VRTK_ControllerEvents>() : collider.gameObject.GetComponentInParent<VRTK_ControllerEvents>());
        if (controller != null)
        {
                destination = GameObject.Find("PlayerRespawn").transform;
                float distance = Vector3.Distance(transform.position, destination.position);
                VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
                OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
        }
    }
}
