using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform interactablePosition;

    public Transform hidenLever;

    public float maxInteractionDistance = 2f;

    public Transform pickedObject;

    public static PlayerInteract instance;

    private bool leverFinded = false;
    public bool melodyPlayedCorrectly = false;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interaction"))
        {
            PickObject();
        }

        if(pickedObject != null)
        {
            pickedObject.position = interactablePosition.position;
        }

        if(hidenLever.position.y < -20)
        {
            leverFinded = false;
        }
    }

    public void PickObject()
    {
        if (pickedObject != null)
        {
            //Liberamos o obxecto
            pickedObject.GetComponent<Rigidbody>().isKinematic = false;
            pickedObject.parent = null;
            pickedObject = null;
        }
        else
        {
            //Miramos se temos un obxecto Pickeable ó alcance
            pickedObject = CheckForInteractables();
            if (pickedObject != null)
            {
                PlayerMovementScript.instance.interacting = true;
                pickedObject.parent = transform;
                pickedObject.GetComponent<Rigidbody>().isKinematic = true;
                pickedObject.localRotation = Quaternion.identity;
            }
        }
    }

    private Transform CheckForInteractables()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxInteractionDistance))
        {
            Debug.Log(hit.collider.gameObject.tag);
            //Comprobamos si o obxecto é interactuable
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                return hit.collider.transform;
            }
            //Comprobamos o caso de que sexa un hiden
            else if (hit.collider.gameObject.CompareTag("Hiden") && !leverFinded)
            {
                hidenLever.gameObject.SetActive(true);
                leverFinded = true;
                return hidenLever;
            }
            else if (hit.collider.gameObject.CompareTag("Lever"))
            {
                hit.collider.gameObject.GetComponent<LeverSocketScript>().PullLever();
                return null;
            }
            else if (hit.collider.gameObject.CompareTag("Music") && !melodyPlayedCorrectly)
            {
                MusicPanel.instance.CubePlayed(hit.collider.GetComponent<MusicCube>());
                return null;
            }
        }
        return null;
    }
}
