using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform interactablePosition;

    public float maxInteractionDistance = 2f;

    Transform pickedObject;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 5, Color.green);
        if (Input.GetButtonDown("Interaction"))
        {
            Debug.Log("pulsamos E");
            PickObject();
        }
    }

    private void PickObject()
    {
        if (pickedObject != null)
        {
            //Liberamos o obxecto
            Debug.Log("Soltamos o obxecto");
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.parent = null;
            pickedObject = null;
        }
        else
        {
            Debug.Log("Miramos si hai obxecto");
            //Miramos se temos un obxecto Pickeable ó alcance
            pickedObject = CheckForInteractables();
            if (pickedObject != null)
            {
                Debug.Log("Hai obxecto");
                pickedObject.parent = transform;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedObject.position = interactablePosition.position;
                pickedObject.localRotation = Quaternion.identity;
            }
        }
    }

    private Transform CheckForInteractables()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxInteractionDistance))
        {
            //Comprobamos si o obxecto é interactuable
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }
}
