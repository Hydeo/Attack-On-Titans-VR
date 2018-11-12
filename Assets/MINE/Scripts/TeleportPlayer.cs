using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class TeleportPlayer : VRTK_DestinationMarker
{
    public Transform destination;

    private void OnTriggerEnter(Collider collider)
    {
        VRTK_ControllerEvents controller = (collider.GetComponent<VRTK_ControllerEvents>() ? collider.GetComponent<VRTK_ControllerEvents>() : collider.GetComponentInParent<VRTK_ControllerEvents>());
        if (controller != null)
        {

                float distance = Vector3.Distance(transform.position, destination.position);
                VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
                OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
        }
    }
}
