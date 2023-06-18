using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestalSocket : MonoBehaviour
{
    public float forcePower = 20;
    public GameObject finalBoss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("UwUMug"))
        {
            if (PlayerInteract.instance.pickedObject != null)
            {
                PlayerInteract.instance.PickObject();
            }
            other.GetComponent<Transform>().position = transform.position;
            other.GetComponent<Transform>().rotation = transform.rotation;

            finalBoss.SetActive(true);
        }
        else if(other.gameObject.CompareTag("Interactable"))
        {
            if (PlayerInteract.instance.pickedObject != null)
            {
                PlayerInteract.instance.PickObject();
            }
            other.GetComponent<Transform>().position = transform.position;
            other.GetComponent<Transform>().rotation = transform.rotation;
            other.GetComponent<Rigidbody>().AddForce((transform.up + transform.forward) * forcePower, ForceMode.Impulse);
        }
    }
}
