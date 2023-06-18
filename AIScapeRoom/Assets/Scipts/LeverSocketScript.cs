using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSocketScript : MonoBehaviour
{
    public Transform leverUpPosition;
    public Transform leverDownPosition;
    public Animator GateAnimator;

    private bool leverPlaced = false;
    private Transform leverTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LeverScript>() != null)
        {
            if (PlayerInteract.instance.pickedObject != null)
            {
                PlayerInteract.instance.PickObject();
            }
            leverTransform = other.transform;
            other.GetComponent<Rigidbody>().isKinematic = true;
            leverTransform.position = leverUpPosition.position;
            leverTransform.rotation = leverUpPosition.rotation;
            leverPlaced = true;
            leverTransform.tag = "Lever";
        }
    }
    public void PullLever()
    {
        if (!leverPlaced)
        {
            return;
        }
        leverTransform.localPosition = leverDownPosition.localPosition;
        leverTransform.localRotation = leverDownPosition.localRotation;
        GateAnimator.SetTrigger("openGate");
    }
}
